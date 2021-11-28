namespace SurfsUp.DataProvider.Providers.Bafu
{
    public class BafuData
    {
        public double OutflowCurrent { get; set; }

        public double OutflowMax24hours { get; set; }

        public double DegreeCelsius { get; set; }

        public ISet<DayOfWeek> Dates { get; set; } = new HashSet<DayOfWeek>();
    }
}
