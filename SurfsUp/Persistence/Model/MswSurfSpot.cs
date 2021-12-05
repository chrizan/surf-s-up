using System.ComponentModel.DataAnnotations;

namespace SurfsUp.Persistence.Model
{
    public class MswSurfSpot
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public int FullStars { get; set; }

        public int BlurredStars { get; set; }
    }
}
