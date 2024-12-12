using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.IdentityCustomer;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class UserSecurityStorage : IBaseStorage<UserSecurity>
{
    public readonly ApplicationDbContext _context;

    public UserSecurityStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserSecurity> AddAsync(UserSecurity entity)
    {
        var result = await _context.UserSecurities.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<UserSecurity> UpdateAsync(UserSecurity entity)
    { 
        _context.UserSecurities.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(UserSecurity entity)
    {
        _context.UserSecurities.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<UserSecurity> GetByIdAsync(Guid id)
    {
        var entity= await _context.UserSecurities.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<UserSecurity> GetAllAsync()
    {
        return _context.UserSecurities.AsQueryable();
    }
}