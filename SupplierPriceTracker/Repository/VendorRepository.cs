﻿using Microsoft.EntityFrameworkCore;
using SupplierPriceTracker.Data;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Repository
{
    public class VendorRepository (ApplicationDbContext context) : IVendorRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<bool> AddAsync(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Vendor vendor)
        {
            vendor.IsDeleted = true;
            _context.Update(vendor);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            return await _context.Vendors.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            int saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateAsync(Vendor vendor)
        {
            _context.Vendors.Update(vendor);
            return await SaveAsync();
        }
    }
}