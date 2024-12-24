public sealed class DataStore
{
    private static readonly DataStore instance = new DataStore();

    public string ManagerEmail = "admin@gmail.com", ManagerPassword = "admin", MangerToken = "12345";

    private readonly List<Room> rooms = new();
    public readonly List<Worker> workers = new();
    private readonly List<Resident> residents = new();

    private DataStore()
    {
        InitializeData();
    }

    public static DataStore Instance => instance;

    public IReadOnlyList<Room> Rooms => rooms;
    public IReadOnlyList<Resident> Residents => residents;

    private void InitializeData()
    {
        for (int i = 1; i <= 6; i++)
        {
            rooms.Add(new Room
            {
                RoomNumber = i,
                Type = "Single",
                IsOccupied = false,
                BasePrice = 50
            });
        }
        for (int i = 7; i <= 14; i++)
        {
            rooms.Add(new Room
            {
                RoomNumber = i ,
                Type = "Double",
                IsOccupied = false,
                BasePrice = 75
            });
        }
        for (int i = 15; i <= 21; i++)
        {
            rooms.Add(new Room
            {
                RoomNumber = i,
                Type = "Triple",
                IsOccupied = false,
                BasePrice = 100
            });
        }



        workers.Add(new Worker { Id = "1", Name = "Alice", email = "alice@gmail.com", Password = "1234", Contact = "123-456-7890", Salary = 50000, JobTitle = "receptionist", Token = "abc123" });

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
    

    public void UpdateRoom(Room room)
    {
        var existingRoom = rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber);
        if (existingRoom != null)
        {
            existingRoom.IsOccupied = room.IsOccupied;
        }
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

    
    

    public decimal CalculateCost(Resident resident, Room room)
    {
        int numberOfNights = (resident.CheckOut.Date - resident.CheckIn.Date).Days; // here take care may lead to 0


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


    public void UpdateRoomStatusAfterCheckout()
    {
        DateTime currentTime = DateTime.Now;

        // Find all rooms that should be marked as unoccupied
        var roomsToUpdate = residents
            .Where(r => r.CheckOut < currentTime)
            .Select(r => r.RoomNumber)
            .Distinct()
            .ToList();

        foreach (var roomNumber in roomsToUpdate)
        {
            // Find the specific room
            var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (room != null)
            {
                // Check if there are no active reservations for this room
                bool hasActiveReservation = residents.Any(r =>
                    r.RoomNumber == roomNumber &&
                    r.CheckIn <= currentTime &&
                    r.CheckOut > currentTime);

                // Update room status only if no active reservations exist
                if (!hasActiveReservation)
                {
                    room.IsOccupied = false;
                }
            }
        }
    }

    
}