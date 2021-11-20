using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class MswSurfSpot
    {
        [Key]
        public string Url { get; set; }

        public string Name { get; set; }

        public int FullStars { get; set; }

        public int BlurredStars { get; set; }
    }
}
