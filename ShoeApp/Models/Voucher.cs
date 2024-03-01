using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class Voucher
    {
        [Key]
        public Guid Id { get; set; }
        public string? VoucherCode { get; set; } //
        public string? Title { get; set; } //
        public int? Quantity { get; set; } // số lượng
        public decimal? Value { get; set; } // giá trị voucher
        public int? Discount_Type { get; set; } // hinh thuc giam gia, 1 là %, 0 là theo tiền mặt
        public int? Minimum_order_value { get; set; } // giá trị đơn hàng tối thiểu ( ví dụ mã áp dụng cho đơn từ 50k )
        public int? MaxDiscountAmount { get; set; } // số tiền giảm tối đa
        public string? Description { get; set; }
        public DateTime? Create_Date { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public bool? Status { get; set; } // đã sử dụng, chưa sử dụng, hết hạn .....
        public virtual List<Order>? Orders { get; set; }
        public virtual List<UserVoucher>? UserVouchers { get; set; }
    }
}
