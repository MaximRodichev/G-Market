using ValidationException = FluentValidation.ValidationException;
using FluentValidation;
namespace GMarket.Domain.Entities;


/// <summary>
/// Базовый класс для всех сущностей домена
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseEntity<T> where T : BaseEntity<T>
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; protected init; }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    #region Методы
    /// <summary>
    /// Выполняет валидацию сущности с использованием указанного валидатора.
    /// </summary>
    /// <param name="validator">Валидатор FluentValidator.</param>
    protected void ValidateEntity(AbstractValidator<T> validator)
    {
        var validationResult = validator.Validate((T)this);
        if (validationResult.IsValid)
        {
            return;
        }
        var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
        throw new ValidationException(errorMessages);
    }

    #region Системный методы

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || GetType() != obj.GetType()) return false;
        
        return Id.Equals(((BaseEntity<T>)obj).Id);
    }

    /// <summary>
    /// Переопределение метода GetHashCode для получения хеш-кода на основе уникального идентификатора.
    /// </summary>
    /// <returns>Хеш-код, основанный на значении идентификатора.</returns>
    public override int GetHashCode() => Id.GetHashCode();
    
    /// <summary>
    /// Сравнивает две сущности
    /// </summary>
    /// <param name="left">Левая сущность</param>
    /// <param name="right">Правая сущность</param>
    /// <returns>Возвращает результат сравнение True or False</returns>
    public static bool operator ==(BaseEntity<T>? left, BaseEntity<T>? right){
        if (left is null) return right is null;
        return left.Equals(right);
    }
    
    
    /// <summary>
    /// Проверяет неравенство двух сущностей
    /// </summary>
    /// <param name="left">Левая сущность</param>
    /// <param name="right">Правая сущность</param>
    /// <returns>True для неравентва сущностей</returns>
    public static bool operator !=(BaseEntity<T>? left, BaseEntity<T>? right){
        return !(left == right);
    }
    
    #endregion

    #endregion
    
}