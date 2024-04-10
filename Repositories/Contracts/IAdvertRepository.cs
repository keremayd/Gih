using Entities;

namespace Repositories.Contracts;

public interface IAdvertRepository
{
    IQueryable<Advert> GetAdvert();
    Task<Advert?> GetAdvertByIdAsync(int id);
    Task<IEnumerable<Advert>> GetAdvertByAddressAsync(string address);
    Task<IEnumerable<Advert>> GetAdvertByRestaurantIdAsync(int id);
    void CreateAdvert(Advert advert);
    void UpdateAdvert(Advert advert);
    void DeleteAdvert(Advert advert);
}