using System.ComponentModel.DataAnnotations;

namespace SurfsUp.Persistence.Model
{
    public class BafuSurfSpot
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public double Outflow { get; set; }
    }
}
