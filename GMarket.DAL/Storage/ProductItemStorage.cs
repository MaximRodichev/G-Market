using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.Market;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class ProductItemStorage : IBaseStorage<ProductItem>
{
    public readonly ApplicationDbContext _context;

    public ProductItemStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProductItem> AddAsync(ProductItem entity)
    {
        var result = await _context.ProductItems.AddAsync(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<ProductItem> UpdateAsync(ProductItem entity)
    { 
        _context.ProductItems.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(ProductItem entity)
    {
        _context.ProductItems.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductItem> GetByIdAsync(Guid id)
    {
        var entity= await _context.ProductItems.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<ProductItem> GetAllAsync()
    {
        return _context.ProductItems.AsQueryable();
    }
}