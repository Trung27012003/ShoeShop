using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }
        public string? BrandName { get; set; }
        public virtual List<Product>? Product { get; set; }
    }
}
