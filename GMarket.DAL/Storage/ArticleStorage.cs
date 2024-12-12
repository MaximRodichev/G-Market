using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.Forum;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class ArticleStorage : IBaseStorage<Article>
{
    public readonly ApplicationDbContext _context;

    public ArticleStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Article> AddAsync(Article entity)
    {
        var result = await _context.Articles.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<Article> UpdateAsync(Article entity)
    { 
        _context.Articles.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Article entity)
    {
        _context.Articles.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Article> GetByIdAsync(Guid id)
    {
        var entity= await _context.Articles.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<Article> GetAllAsync()
    {
        return _context.Articles.AsQueryable();
    }
}