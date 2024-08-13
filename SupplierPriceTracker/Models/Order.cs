using System.ComponentModel.DataAnnotations;

namespace SupplierPriceTracker.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateOnly OrderDate;

        [Required]
        public int VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
