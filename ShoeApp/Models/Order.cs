﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? User_Reference_Id { get; set; } // id người tham chiếu
        public Guid? OrderStatusId { get; set; }
        public string? PaymentType { get; set; }
        public Guid? VoucherId { get; set; }
        public string? OrderCode { get; set; } // mã bill
        public string? RecipientName { get; set; }
        public string? RecipientAddress { get; set; }// địa chỉ
        public string? RecipientPhone { get; set; }// sdt
        public decimal? TotalAmout { get; set; } // tổng tiền trước khi áp dụng
        public decimal? VoucherValue { get; set; } // giá trị voucher
        public decimal? TotalAmoutAfterApplyingVoucher { get; set; } // giá sau khi áp voucher
        public decimal? ShippingFee { get; set; } // phí ship
        public DateTime? Create_Date { get; set; } // ngày tạo bill
        public DateTime? Update_Date { get; set; } // ngày update bill
        public DateTime? Ship_Date { get; set; } // ngày Ship
        public DateTime? Payment_Date { get; set; } // ngày thanh toán
        public DateTime? Delivery_Date { get; set; } // ngày nhận hàng
        public string? Description { get; set; } // mô tả
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus? OrderStatus { get; set; }
        [ForeignKey("VoucherId")]

        public Voucher? Voucher { get; set; }

        public virtual List<OrderItem>? OrderItem { get; set; }
    }
}
