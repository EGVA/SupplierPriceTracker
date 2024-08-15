using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Interfaces.Repository
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendor>> GetAllAsync();
        Task<IEnumerable<Vendor>> SearchVendor(string name, bool? isDeleted = false);
        Task<bool> AddAsync(Vendor vendor);
        Task<bool> DeleteAsync(Vendor vendor);
        Task<bool> UpdateAsync(Vendor vendor);
        Task<bool> SaveAsync();
    }
}
