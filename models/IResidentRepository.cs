public interface IResidentRepository
{
    void AddResident(Resident resident);
    bool EditResident(string residentId, Resident updatedResident);
    bool DeleteResident(string residentId);
    decimal CalculateCost(Resident resident, Room room);
    IReadOnlyList<Resident> GetAllResidents();
}
public class ResidentRepository : IResidentRepository
{
    private readonly DataStore dataStore = DataStore.Instance;

    public ResidentRepository( )
    {
    }
    public IReadOnlyList<Resident> GetAllResidents()
    {
        return dataStore.residents;
    }
    
    public void AddResident(Resident resident)
    {

        ITokenService realService = new TokenService();
        ITokenService proxy = new TokenServiceProxy(realService);
        resident.Id = proxy.CreateUniqueiId();
        dataStore.residents.Add(resident);
        var roomExists = dataStore.rooms.Where(r => r.RoomNumber == resident.RoomNumber).FirstOrDefault();
        roomExists.IsOccupied = true;
    }

    public bool EditResident(string residentId, Resident updatedResident)
    {
        var resident = dataStore.residents.FirstOrDefault(r => r.Id == residentId);

        if (resident == null)
        {
            return false; 
        }

        resident.Name = updatedResident.Name;
        resident.phoneNumber = updatedResident.phoneNumber;
        resident.email = updatedResident.email;
        resident.BoardingType = updatedResident.BoardingType;
        resident.CheckIn = updatedResident.CheckIn;
        resident.CheckOut = updatedResident.CheckOut;
        resident.RoomNumber = updatedResident.RoomNumber;
        return true;
    }
    public bool DeleteResident(string residentId)
    {
        var resident = dataStore.residents.FirstOrDefault(r => r.Id == residentId);
        if (resident == null)
        {
            return false; 
        }

        DateTime now = DateTime.Now;

        if (resident.CheckOut > now)
        {
            // Update room status to not occupied
            var assignedRoom = dataStore.rooms.FirstOrDefault(r => r.RoomNumber == resident.RoomNumber);
            if (assignedRoom != null)
            {
                assignedRoom.IsOccupied = false;
            }

        }

        dataStore.residents.Remove(resident); 
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

}
