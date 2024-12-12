using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.Market;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class ProductStorage : IBaseStorage<Product>
{
    public readonly ApplicationDbContext _context;

    public ProductStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product> AddAsync(Product entity)
    {
        var result = await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<Product> UpdateAsync(Product entity)
    { 
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Product entity)
    {
        _context.Products.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var entity= await _context.Products.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<Product> GetAllAsync()
    {
        return _context.Products.AsQueryable();
    }
}