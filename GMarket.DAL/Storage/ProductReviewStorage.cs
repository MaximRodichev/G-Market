using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.Market;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class ProductReviewStorage : IBaseStorage<ProductReview>
{
    public readonly ApplicationDbContext _context;

    public ProductReviewStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProductReview> AddAsync(ProductReview entity)
    {
        var result = await _context.ProductReviews.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<ProductReview> UpdateAsync(ProductReview entity)
    { 
        _context.ProductReviews.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(ProductReview entity)
    {
        _context.ProductReviews.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductReview> GetByIdAsync(Guid id)
    {
        var entity= await _context.ProductReviews.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<ProductReview> GetAllAsync()
    {
        return _context.ProductReviews.AsQueryable();
    }
}