public interface IPeriodStrategy
{
    DateTime GetStartDate(DateTime endDate);
    string GetPeriodName();
}

public class WeeklyPeriodStrategy : IPeriodStrategy
{
    public DateTime GetStartDate(DateTime endDate) => endDate.AddDays(-7);
    public string GetPeriodName() => "Weekly";
}

public class MonthlyPeriodStrategy : IPeriodStrategy
{
    public DateTime GetStartDate(DateTime endDate) => endDate.AddMonths(-1);
    public string GetPeriodName() => "Monthly";
}

public class AnnualPeriodStrategy : IPeriodStrategy
{
    public DateTime GetStartDate(DateTime endDate) => endDate.AddYears(-1);
    public string GetPeriodName() => "Annual";
}

public class HotelIncomeReport
{
    private readonly IEnumerable<Resident> residents;
    private readonly IEnumerable<Room> rooms;
    private readonly IPeriodStrategy periodStrategy;

    public HotelIncomeReport(
        IEnumerable<Resident> residents, 
        IEnumerable<Room> rooms, 
        string period)
    {
        this.residents = residents;
        this.rooms = rooms;
        this.periodStrategy = GetPeriodStrategy(period);
    }

    private IPeriodStrategy GetPeriodStrategy(string period) => period.ToLower() switch
    {
        "weekly" => new WeeklyPeriodStrategy(),
        "monthly" => new MonthlyPeriodStrategy(),
        "annual" => new AnnualPeriodStrategy(),
        _ => throw new ArgumentException("Invalid period. Use 'weekly', 'monthly', or 'annual'.")
    };

    public List<string> GenerateReport()
    {
        DateTime endDate = DateTime.Now;
        DateTime startDate = periodStrategy.GetStartDate(endDate);
        var reportLines = new List<string>();
        decimal totalIncome = 0;

        // Add report header
        reportLines.Add($"Hotel Income Report - {periodStrategy.GetPeriodName().ToUpper()} Period");
        reportLines.Add($"Report Generated: {endDate:yyyy-MM-dd HH:mm:ss}");
        reportLines.Add($"Period Start: {startDate:yyyy-MM-dd}");
        reportLines.Add($"Period End: {endDate:yyyy-MM-dd}");
        reportLines.Add("--------------------------------------------");

        foreach (var resident in residents)
        {
            // Check if the stay falls within the selected period
            if (resident.CheckOut >= startDate && resident.CheckIn < endDate)
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == resident.RoomNumber);
                if (room != null)
                {
                    var (residentIncome, residentDetails) = CalculateResidentIncome(
                        resident, room, startDate, endDate);
                    
                    totalIncome += residentIncome;
                    reportLines.AddRange(residentDetails);
                }
            }
        }

        reportLines.Add($"TOTAL INCOME FOR {periodStrategy.GetPeriodName().ToUpper()} PERIOD: ${totalIncome:F2}");
        return reportLines;
    }

    private (decimal income, List<string> details) CalculateResidentIncome(
        Resident resident, 
        Room room, 
        DateTime startDate, 
        DateTime endDate)
    {
        var details = new List<string>();
        
        // Calculate boarding cost
        decimal boardingCost = resident.BoardingType switch
        {
            "FullBoard" => 50,
            "HalfBoard" => 30,
            "BedAndBreakfast" => 15,
            _ => throw new ArgumentException("Invalid boarding type")
        };

        // Adjust dates to fit the reporting period
        DateTime effectiveCheckIn = resident.CheckIn < startDate ? startDate : resident.CheckIn;
        DateTime effectiveCheckOut = resident.CheckOut > endDate ? endDate : resident.CheckOut;
        TimeSpan duration = effectiveCheckOut - effectiveCheckIn;
        decimal residentIncome = (room.BasePrice + boardingCost) * duration.Days;

        // Create resident details report
        details.Add($"Resident Details:");
        details.Add($"  Name: {resident.Name}");
        details.Add($"  Room Number: {resident.RoomNumber}");
        details.Add($"  Boarding Type: {resident.BoardingType}");
        details.Add($"  Check-In: {resident.CheckIn:yyyy-MM-dd}");
        details.Add($"  Check-Out: {resident.CheckOut:yyyy-MM-dd}");
        details.Add($"  Effective Stay Period: {effectiveCheckIn:yyyy-MM-dd} to {effectiveCheckOut:yyyy-MM-dd}");
        details.Add($"  Stay Duration: {duration.Days} days");
        details.Add($"  Room Base Price: ${room.BasePrice} per day");
        details.Add($"  Boarding Cost: ${boardingCost} per day");
        details.Add($"  Total Resident Income: ${residentIncome:F2}");
        details.Add("--------------------------------------------");

        return (residentIncome, details);
    }
}