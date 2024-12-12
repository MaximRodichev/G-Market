using GMarket.Domain.Types;

namespace GMarket.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T> where T : class
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}