using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class OrderStatus
    {
        [Key]
        public Guid Id { get; set; }
        public string? OrderStatusName { get; set; }
        public virtual List<Order>? Order { get; set; }
    }
}
