public class Resident
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string phoneNumber { get; set; }
    public string email { get; set; }

    public string BoardingType { get; set; }
    public DateTime CheckIn { get; set; } = DateTime.Now;
    public DateTime CheckOut { get; set; } 
    public int RoomNumber { get; set; }
    //--
    public int? NumberOfNights { get; set; }
    public decimal? TotalPrice { get; set; }
}