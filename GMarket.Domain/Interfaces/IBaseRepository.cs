namespace GMarket.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    IQueryable<T> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}