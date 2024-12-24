public interface IRoomRepository
{
    IReadOnlyList<Room> GetAllRooms();
    void AddRoom(Room room);
    void UpdateRoom(Room room);
    void UpdateRoomStatusAfterCheckout();
}
public class RoomRepository : IRoomRepository
{
    private readonly DataStore dataStore = DataStore.Instance;

    public RoomRepository()
    {

    }
    

    // Get all rooms
    public IReadOnlyList<Room> GetAllRooms()
    {
        return dataStore.rooms;
    }

    public void AddRoom(Room room) => dataStore.rooms.Add(room);
    

    public void UpdateRoom(Room room)
    {
        var existingRoom = dataStore.rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber);
        if (existingRoom != null)
        {
            existingRoom.IsOccupied = room.IsOccupied;
        }
    }

    public void UpdateRoomStatusAfterCheckout()
    {
        DateTime currentTime = DateTime.Now;

        var roomsToUpdate = dataStore.residents
            .Where(r => r.CheckOut < currentTime)
            .Select(r => r.RoomNumber)
            .Distinct()
            .ToList();

        foreach (var roomNumber in roomsToUpdate)
        {
            var room = dataStore.rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (room != null)
            {
                bool hasActiveReservation = dataStore.residents.Any(r =>
                    r.RoomNumber == roomNumber &&
                    r.CheckIn <= currentTime &&
                    r.CheckOut > currentTime);

                if (!hasActiveReservation)
                {
                    room.IsOccupied = false;
                }
            }
        }
    }

}
