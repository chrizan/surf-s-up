using System.ComponentModel.DataAnnotations;

namespace SurfsUp.Persistence.Model
{
    public class BafuSurfSpot
    {
        [Key]
        public string Url { get; set; }

        public string Name { get; set; }

        public double Outflow { get; set; }
    }
}
