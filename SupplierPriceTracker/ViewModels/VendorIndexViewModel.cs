using SupplierPriceTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace SupplierPriceTracker.ViewModels
{
	public class VendorIndexViewModel
	{
        [Required]
        public Vendor? Vendor { get; set; }
        public IEnumerable<Vendor>? ViewVendors { get; set; }
    }
}
