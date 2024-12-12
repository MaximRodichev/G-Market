using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Entities.Market;
using Microsoft.EntityFrameworkCore;

namespace GMarket.DAL.Storage;

public class OrderItemStorage : IBaseStorage<OrderItem>
{
    public readonly ApplicationDbContext _context;

    public OrderItemStorage(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<OrderItem> AddAsync(OrderItem entity)
    {
        var result = await _context.OrderItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<OrderItem> UpdateAsync(OrderItem entity)
    { 
        _context.OrderItems.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(OrderItem entity)
    {
        _context.OrderItems.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderItem> GetByIdAsync(Guid id)
    {
        var entity= await _context.OrderItems.FirstOrDefaultAsync(x=>x.Id==id);
        return entity;
    }

    public IQueryable<OrderItem> GetAllAsync()
    {
        return _context.OrderItems.AsQueryable();
    }
}