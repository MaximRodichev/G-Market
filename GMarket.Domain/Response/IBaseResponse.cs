namespace GMarket.Domain.Response;

public interface IBaseResponse<T>
{
    T Data { get; set; }
}