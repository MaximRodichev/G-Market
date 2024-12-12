using System.ComponentModel;

namespace GMarket.Domain.Types;

/// <summary>
/// Статусы заказа
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Ожидает оплаты
    /// </summary>
    [Description("Ожидает оплаты")]
    AwaitingPayment,
    /// <summary>
    /// Собирается
    /// </summary>
    [Description("Собирается")]
    Pending,
    /// <summary>
    /// В пути
    /// </summary>
    [Description("В пути")]
    Shipped,
    /// <summary>
    /// Завершенный
    /// </summary>
    [Description("Завершенный")]
    Completed,
    /// <summary>
    /// Возврат средств
    /// </summary>
    [Description("Возврат средств")]
    Refunded,
    /// <summary>
    /// Отмененный
    /// </summary>
    [Description("Отменненый")]
    Canceled,
}