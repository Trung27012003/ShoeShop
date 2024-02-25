using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? CartId { get; set; }
        public Guid? Product_Detail_ID { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }
        [ForeignKey("Product_Detail_ID")]
        public ProductDetail? ProductDetail { get; set; }
    }
}
