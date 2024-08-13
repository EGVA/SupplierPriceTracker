using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierPriceTracker.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal TotalValue { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
