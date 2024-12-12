namespace GMarket.DAL.Interfaces;

/// <summary>
/// Общий репозиторий для всех CRUD операций
/// </summary>
/// <typeparam name="T">Сущность Generic</typeparam>
public interface IBaseStorage<T>
{
    /// <summary>
    /// Добавление
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> AddAsync(T entity);
    /// <summary>
    /// Обновление
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> UpdateAsync(T entity);
    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(T entity);
    /// <summary>
    /// Получение по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> GetByIdAsync(Guid id);
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    /// <returns></returns>
    IQueryable<T> GetAllAsync();
}