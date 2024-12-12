using GMarket.Domain.Entities.Market;

namespace GMarket.Domain.Entities.IdentityCustomer;

public class FavoriteCategory : BaseEntity<FavoriteCategory>
{
    public CustomerContext User { get; init; }
    public string CategoryName { get; set; }
    public ICollection<Guid> ProductIds { get; set; }
}