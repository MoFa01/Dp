public interface IRoomObserver
{
    void Update(Room room);
}

public class RoomStatusManager
{
    private readonly List<IRoomObserver> observers = new();
    
    public void Attach(IRoomObserver observer) => observers.Add(observer);
    public void Detach(IRoomObserver observer) => observers.Remove(observer);
    
    public void NotifyRoomStatusChange(Room room)
    {
        foreach (var observer in observers)
        {
            observer.Update(room);
        }
    }
}

public class RoomStatusLogger : IRoomObserver
{
    public void Update(Room room)
    {
        Console.WriteLine($"Room {room.RoomNumber} status changed. Is Occupied: {room.IsOccupied}");
    }
}