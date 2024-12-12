using GMarket.Domain.Validators;

namespace GMarket.Domain.ValueObjects;

/// <summary>
/// Объект реализующий данные для доставки товара
/// </summary>
public class Address : BaseValueObject
{

    /// <summary>
    /// Конструктор для EFCore
    /// </summary>
    public Address()
    {
        
    }
    
    /// <summary>
    /// Базовый конструктор
    /// </summary>
    /// <param name="phone">Номерт елефона</param>
    /// <param name="house">Дом</param>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="city">Город</param>
    /// <param name="street">Улица</param>
    /// <param name="postalCode">Почтовый код</param>
    /// <param name="country">Страна</param>
    public Address(string phone, string house, string name, string surname, string city, string street, string postalCode, string country)
    {
        Phone=phone;
        Name=name;
        Surname=surname;
        Street = street;
        City = city;
        House = house;
        PostCode = postalCode;
        
        ValidateValueObject(new AddressValidator());
    }
    
    
    /// <summary>
    /// Номер телефона, по которому можно найти заказчика
    /// </summary>
    public string Phone { get; private set; }
    /// <summary>
    /// Имя заказчика
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Фамилия заказчика
    /// </summary>
    public string Surname { get; private set; }
    /// <summary>
    /// Улица куда доставлять
    /// </summary>
    public string Street { get; private set; }
    /// <summary>
    /// Город, куда отправлять
    /// </summary>
    public string City { get; private set; }
    /// <summary>
    /// Номер дома
    /// </summary>
    public string House { get; private set; }
    /// <summary>
    /// Почтовый код
    /// </summary>
    public string PostCode { get; private set; }
}