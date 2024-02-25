using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class Adress
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? Province { get; set; } // Tỉnh/Thành phố
        public string? District { get; set; } // Quận/Huyện
        public string? Ward { get; set; } // Xã
        public bool? IsDefault { get; set; } // is default
        public string? DescriptionAddress { get; set; } // is địa chỉ chi tiết
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
