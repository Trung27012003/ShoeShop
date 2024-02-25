using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public virtual List<Product>? Product { get; set; }
    }

}
