public class Room
{
    public int RoomNumber { get; set; }
    public string Type { get; set; }
    public bool IsOccupied { get; set; }
    public decimal BasePrice { get; set; }
}

//----------
public class SingleRoom : Room
{
    public SingleRoom()
    {
        Type = "Single";
        BasePrice = 50.0m;
    }
}

public class DoubleRoom : Room
{
    public DoubleRoom()
    {
        Type = "Double";
        BasePrice = 75.0m;
    }
}

public class TripleRoom : Room
{
    public TripleRoom()
    {
        Type = "Triple";
        BasePrice = 100.0m;
    }
}

// Factory for creating rooms
public static class RoomFactory
{
    public static Room CreateRoom(string roomType, int roomNumber, bool isOccupied = false)
    {
        Room room = roomType switch
        {
            "Single" => new SingleRoom(),
            "Double" => new DoubleRoom(),
            "Triple" => new TripleRoom(),
            _ => throw new ArgumentException("Invalid room type.")
        };

        room.RoomNumber = roomNumber;
        room.IsOccupied = isOccupied;
        return room;
    }
}