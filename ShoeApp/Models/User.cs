using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Points { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public Guid RankId { get; set; }
        [ForeignKey("RankId")]
        public virtual Rank? Rank { get; set; }
        public virtual List<Adress>? Adresses { get; set; }
        public virtual List<UserVoucher>? VoucherUsers { get; set; }
        public virtual List<Post>? Posts { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
}
