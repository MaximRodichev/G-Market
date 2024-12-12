namespace GMarket.Domain;

public class ValidationMessage
{
    public static string Required = "{PropertyName} обязателен.";
    public static string EmailInvalid = "Введен неккоректный Email";
    public static string MaxValue = "{PropertyName} больше {MaxLenght}";
    public static string MinValue = "{PropertyName} меньше {MinLenght}";
    public static string NotEqual = "{PropertyName} не равен {ComparisonValue}";
    public static string OnlyLetters = "{PropertyName} должен содержать только буквы латинского или только русского языка";
    public static string OnlyNumbers = "{PropertyName} должен содержать только цифры";
    public static string PhoneNumber = "{PropertyName} не является номеров телефона";
    public static string GreaterThanOrEqualTo = "{PropertyName} должен быть равен или больше {ComparisonValue}";
    public static string LessThanOrEqualTo = "{PropertyName} должен быть равен или меньше {ComparisonValue}";
}