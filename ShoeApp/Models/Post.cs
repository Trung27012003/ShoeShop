using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? Tittle { get; set; }
        public string? TittleImage { get; set; }
        public string? Contents { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
