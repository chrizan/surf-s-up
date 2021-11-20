using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class BafuSurfSpot
    {
        [Key]
        public string Url { get; set; }

        public string Name { get; set; }

        public double Outflow { get; set; }
    }
}
