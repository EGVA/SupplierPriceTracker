using System.ComponentModel.DataAnnotations;

namespace SupplierPriceTracker.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
