using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.IdentityCustomer;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class FavoriteCategoryStorage : IBaseStorage<FavoriteCategory>
{
    public readonly ApplicationDbContext _context;

    public FavoriteCategoryStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<FavoriteCategory> AddAsync(FavoriteCategory entity)
    {
        var result = await _context.FavoriteCategories.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<FavoriteCategory> UpdateAsync(FavoriteCategory entity)
    { 
        _context.FavoriteCategories.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(FavoriteCategory entity)
    {
        _context.FavoriteCategories.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<FavoriteCategory> GetByIdAsync(Guid id)
    {
        var entity= await _context.FavoriteCategories.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<FavoriteCategory> GetAllAsync()
    {
        return _context.FavoriteCategories.AsQueryable();
    }
}