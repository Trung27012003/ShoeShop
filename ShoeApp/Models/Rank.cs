using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class Rank
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? PointsMin { get; set; }
        public int? PoinsMax { get; set; }
        public string? Description { get; set; }
        public virtual List<User>? Users { get; set; }
    }
}
