using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.IdentityCustomer;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class CustomerContextStorage : IBaseStorage<CustomerContext>
{
    public readonly ApplicationDbContext _context;

    public CustomerContextStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<CustomerContext> AddAsync(CustomerContext entity)
    {
        var result = await _context.CustomerContexts.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<CustomerContext> UpdateAsync(CustomerContext entity)
    { 
        _context.CustomerContexts.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(CustomerContext entity)
    {
        _context.CustomerContexts.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<CustomerContext> GetByIdAsync(Guid id)
    {
        var entity= await _context.CustomerContexts.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<CustomerContext> GetAllAsync()
    {
        return _context.CustomerContexts.AsQueryable();
    }
}