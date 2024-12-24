public sealed class DataStore
{
    private static readonly DataStore instance = new DataStore();

    public string ManagerEmail = "admin@gmail.com", ManagerPassword = "admin", MangerToken = "12345";

    public readonly List<Room> rooms = new();
    public readonly List<Worker> workers = new();
    public readonly List<Resident> residents = new();

    private DataStore()
    {
        InitializeData();
    }

    public static DataStore Instance => instance;

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
   
}