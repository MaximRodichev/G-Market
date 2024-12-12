using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.Forum;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class ArticleCommentaryStorage : IBaseStorage<ArticleCommentary>
{
    public readonly ApplicationDbContext _context;

    public ArticleCommentaryStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ArticleCommentary> AddAsync(ArticleCommentary entity)
    {
        var result = await _context.ArticleCommentaries.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<ArticleCommentary> UpdateAsync(ArticleCommentary entity)
    { 
        _context.ArticleCommentaries.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(ArticleCommentary entity)
    {
        _context.ArticleCommentaries.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ArticleCommentary> GetByIdAsync(Guid id)
    {
        var entity= await _context.ArticleCommentaries.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<ArticleCommentary> GetAllAsync()
    {
        return _context.ArticleCommentaries.AsQueryable();
    }
}