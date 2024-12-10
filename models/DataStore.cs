public sealed class DataStore
{
    private static readonly DataStore instance = new DataStore();

    public string ManagerEmail = "admin@gmail.com", ManagerPassword = "admin", MangerToken = "12345";

    private readonly List<Room> rooms = new();
    private readonly List<Worker> workers = new();
    private readonly List<Resident> residents = new();
    private Dictionary<string, Worker> _authorizedWorkers;

    private DataStore()
    {
        InitializeData();
    }

    public static DataStore Instance => instance;

    public IReadOnlyList<Room> Rooms => rooms;
    public IReadOnlyList<Resident> Residents => residents;

    private void InitializeData()
    {
        _authorizedWorkers = new Dictionary<string, Worker>();


        // Initialize with some sample data

        // Add some rooms
        for (int i = 1; i <= 6; i++)
        {
            rooms.Add(new Room
            {
                RoomNumber = i,
                Type = "Single",
                IsOccupied = false,
                BasePrice = 50
            });
            rooms.Add(new Room
            {
                RoomNumber = i + 6,
                Type = "Double",
                IsOccupied = false,
                BasePrice = 75
            });
            rooms.Add(new Room
            {
                RoomNumber = i + 13,
                Type = "Triple",
                IsOccupied = false,
                BasePrice = 100
            });

        }



        workers.Add(new Worker { Id = "1", Name = "Alice", email = "alice@example.com", Password = "1234", Contact = "123-456-7890", Salary = 50000, JobTitle = "Manager", Token = "abc123" });

    }
    public void AddResident(Resident resident)
    {

        ITokenService realService = new TokenService();
        ITokenService proxy = new TokenServiceProxy(realService);
        resident.Id = proxy.CreateUniqueiId();
        residents.Add(resident);
        var roomExists = rooms.Where(r => r.RoomNumber == resident.RoomNumber).FirstOrDefault();
        roomExists.IsOccupied = true;
        //dataStore.UpdateRoom(room);
    }


    public void AddRoom(Room room) => rooms.Add(room);
    public List<string> ViewResidentInformation()
    {
        var residentInfoList = new List<string>();

        foreach (var resident in residents)
        {
            // Fetch the room details
            var room = rooms.FirstOrDefault(r => r.RoomNumber == resident.RoomNumber);

            // Calculate stay duration
            TimeSpan stayDuration = resident.CheckOut - resident.CheckIn;

            // Build resident information
            string residentInfo = $"Resident ID: {resident.Id}\n" +
                                  $"Name: {resident.Name}\n" +
                                  $"Phone: {resident.phoneNumber}\n" +
                                  $"Email: {resident.email}\n" +
                                  $"Boarding Type: {resident.BoardingType}\n" +
                                  $"Room Number: {resident.RoomNumber} ({room?.Type ?? "N/A"})\n" +
                                  $"Base Price: {(room?.BasePrice.ToString("C") ?? "N/A")}\n" +
                                  $"Check-In: {resident.CheckIn.ToShortDateString()}\n" +
                                  $"Check-Out: {resident.CheckOut.ToShortDateString()}\n" +
                                  $"Stay Duration: {stayDuration.Days} days\n";

            // Add to the list
            residentInfoList.Add(residentInfo);
        }

        return residentInfoList;
    }

    public void UpdateRoom(Room room)
    {
        var existingRoom = rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber);
        if (existingRoom != null)
        {
            existingRoom.IsOccupied = room.IsOccupied;
        }
    }


    //--------------------------> Role MANGER for:  WORKER <-------------------------------
    public IReadOnlyList<Worker> Workers => workers;

    public void AddWorker(Worker worker)
    {

        ITokenService realService = new TokenService();
        ITokenService proxy = new TokenServiceProxy(realService);
        worker.Id = proxy.CreateUniqueiId();


        string token1 = proxy.CreateToken(worker.Id);
        worker.Token = token1;
        workers.Add(worker);
        _authorizedWorkers[token1] = worker;
    }
    public void UpdateWorker(Worker worker)
    {
        var existingWorker = workers.FirstOrDefault(w => w.Id == worker.Id);
        if (existingWorker != null)
        {
            existingWorker.Name = worker.Name;
            existingWorker.Contact = worker.Contact;
            existingWorker.Salary = worker.Salary;
            existingWorker.JobTitle = worker.JobTitle;
            existingWorker.email = worker.email;
            existingWorker.Password = worker.Password;
        }
    }

    public bool DeleteWorker(string workerId)
    {
        var worker = workers.FirstOrDefault(w => w.Id == workerId);
        if (worker != null)
        {
            workers.Remove(worker);
            _authorizedWorkers.Remove(worker.Token);
            return true;
        }
        return false;
    }


    //--------------------------> Role MANGER: Track Hotel Income: <-------------------------------
    public decimal TrackHotelIncome(string period)
    {
        DateTime now = DateTime.Now;
        DateTime startDate = period.ToLower() switch
        {
            "weekly" => now.AddDays(-7),
            "monthly" => now.AddMonths(-1),
            "annual" => now.AddYears(-1),
            _ => throw new ArgumentException("Invalid period. Use 'weekly', 'monthly', or 'annual'.")
        };

        decimal totalIncome = 0;

        foreach (var resident in residents)
        {
            // Check if the stay falls within the selected period
            if (resident.CheckOut >= startDate && resident.CheckIn <= now)
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == resident.RoomNumber);

                if (room != null)
                {
                    // Determine boarding cost
                    decimal boardingCost = resident.BoardingType switch
                    {
                        "FullBoard" => 50,
                        "HalfBoard" => 30,
                        "BedAndBreakfast" => 15,
                        _ => throw new ArgumentException("Invalid boarding type")
                    };

                    // Adjust dates to fit the reporting period
                    DateTime effectiveCheckIn = resident.CheckIn < startDate ? startDate : resident.CheckIn;
                    DateTime effectiveCheckOut = resident.CheckOut > now ? now : resident.CheckOut;

                    TimeSpan duration = effectiveCheckOut - effectiveCheckIn;

                    // Calculate income for this resident
                    totalIncome += (room.BasePrice + boardingCost) * duration.Days;
                }
            }
        }

        return totalIncome;
    }

    public List<string> TrackHotelIncomeReport(string period)
    {
        DateTime now = DateTime.Now;
        DateTime startDate = period.ToLower() switch
        {
            "weekly" => now.AddDays(-7),
            "monthly" => now.AddMonths(-1),
            "annual" => now.AddYears(-1),
            _ => throw new ArgumentException("Invalid period. Use 'weekly', 'monthly', or 'annual'.")
        };

        decimal totalIncome = 0;
        var reportLines = new List<string>();

        // Add report header
        reportLines.Add($"Hotel Income Report - {period.ToUpper()} Period");
        reportLines.Add($"Report Generated: {now:yyyy-MM-dd HH:mm:ss}");
        reportLines.Add($"Period Start: {startDate:yyyy-MM-dd}");
        reportLines.Add($"Period End: {now:yyyy-MM-dd}");
        reportLines.Add("--------------------------------------------");

        foreach (var resident in residents)
        {
            // Check if the stay falls within the selected period
            if (resident.CheckOut >= startDate && resident.CheckIn < now)
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == resident.RoomNumber);

                if (room != null)
                {
                    // Determine boarding cost
                    decimal boardingCost = resident.BoardingType switch
                    {
                        "FullBoard" => 50,
                        "HalfBoard" => 30,
                        "BedAndBreakfast" => 15,
                        _ => throw new ArgumentException("Invalid boarding type")
                    };

                    // Adjust dates to fit the reporting period
                    DateTime effectiveCheckIn = resident.CheckIn < startDate ? startDate : resident.CheckIn;
                    DateTime effectiveCheckOut = resident.CheckOut > now ? now : resident.CheckOut;

                    TimeSpan duration = effectiveCheckOut - effectiveCheckIn;
                    decimal residentIncome = (room.BasePrice + boardingCost) * duration.Days;

                    totalIncome += residentIncome;

                    // Create a detailed report line for each resident
                    reportLines.Add($"Resident Details:");
                    reportLines.Add($"  Name: {resident.Name}");
                    reportLines.Add($"  Room Number: {resident.RoomNumber}");
                    reportLines.Add($"  Boarding Type: {resident.BoardingType}");
                    reportLines.Add($"  Check-In: {resident.CheckIn:yyyy-MM-dd}");
                    reportLines.Add($"  Check-Out: {resident.CheckOut:yyyy-MM-dd}");
                    reportLines.Add($"  Effective Stay Period: {effectiveCheckIn:yyyy-MM-dd} to {effectiveCheckOut:yyyy-MM-dd}");
                    reportLines.Add($"  Stay Duration: {duration.Days} days");
                    reportLines.Add($"  Room Base Price: ${room.BasePrice} per day");
                    reportLines.Add($"  Boarding Cost: ${boardingCost} per day");
                    reportLines.Add($"  Total Resident Income: ${residentIncome:F2}");
                    reportLines.Add("--------------------------------------------");
                }
            }
        }

        // Add total income summary
        reportLines.Add($"TOTAL INCOME FOR {period.ToUpper()} PERIOD: ${totalIncome:F2}");

        return reportLines;
    }

    public string? IsAuthManger(string username, string password)
    {
        if (username == ManagerEmail && password == ManagerPassword)
        {
            return MangerToken;
        }
        return null;
    }
    public bool IsAuthMangerByToken(string token)
    {
        if (token == MangerToken)
        {
            return true;
        }
        return false;
    }

    //--------------------------> Role WORKER for:  Resident <-------------------------------
    public bool EditResident(string residentId, Resident updatedResident)
    {
        var resident = residents.FirstOrDefault(r => r.Id == residentId);

        if (resident == null)
        {
            return false; // Resident not found
        }

        // Update resident details
        resident.Name = updatedResident.Name;
        resident.phoneNumber = updatedResident.phoneNumber;
        resident.email = updatedResident.email;
        resident.BoardingType = updatedResident.BoardingType;
        resident.CheckIn = updatedResident.CheckIn;
        resident.CheckOut = updatedResident.CheckOut;
        resident.RoomNumber = updatedResident.RoomNumber;

        // // Ensure room assignment consistency
        // var assignedRoom = rooms.FirstOrDefault(r => r.RoomNumber == resident.RoomNumber);
        // if (assignedRoom != null)
        // {
        //     assignedRoom.IsOccupied = true; // Mark the room as occupied
        // }

        return true;
    }
    public bool DeleteResident(string residentId)
    {
        var resident = residents.FirstOrDefault(r => r.Id == residentId);
        if (resident == null)
        {
            return false; // Resident not found
        }

        DateTime now = DateTime.Now;

        if (resident.CheckOut > now)
        {
            // Update room status to not occupied
            var assignedRoom = rooms.FirstOrDefault(r => r.RoomNumber == resident.RoomNumber);
            if (assignedRoom != null)
            {
                assignedRoom.IsOccupied = false;
            }

        }

        residents.Remove(resident); // Remove resident from the list
        return true;
    }

    public string AuthenticateWorker(string email, string password)
    {
        var worker = workers.FirstOrDefault(w => w.email == email && w.Password == password);

        if (worker == null)
        {
            throw new ArgumentException("Invalid email or password.");
        }

        if (_authorizedWorkers.ContainsValue(worker))
        {
            return worker.Token;
        }

        throw new InvalidOperationException("Worker is not authorized.");
    }
    public bool ValidateToken(string token)
    {
        return _authorizedWorkers.ContainsKey(token);
    }

    public decimal CalculateCost(Resident resident, Room room)
    {
        int numberOfNights = (resident.CheckOut - resident.CheckIn).Days + 1; // here take care may lead to 0


        var boardingCost = 0;
        if (resident.BoardingType == "FullBoard")
        {
            boardingCost = 50;
        }
        else if (resident.BoardingType == "HalfBoard")
        {
            boardingCost = 30;
        }
        else if (resident.BoardingType == "BedAndBreakfast")
        {
            boardingCost = 15;
        }
        else
        {
            throw new ArgumentException("Invalid room type");
        }

        return (room.BasePrice + boardingCost) * numberOfNights;
    }

}