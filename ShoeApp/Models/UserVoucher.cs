using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class UserVoucher
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid VoucherId { get; set; }
        public bool? Status { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("VoucherId")]
        public virtual Voucher? Voucher { get; set; }

    }
}
