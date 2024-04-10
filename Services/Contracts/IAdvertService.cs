using Entities;
using Services.DataTransferObjects.Request;

namespace Services.Contracts;

public interface IAdvertService
{
    IQueryable<Advert> GetAdvert();
    Task<Advert?> GetAdvertByIdAsync(int id);
    Task<IEnumerable<Advert>> GetAdvertByAddressAsync(string address); 
    Task<IEnumerable<Advert>> GetAdvertByRestaurantIdAsync(int id);
    Task CreateAdvertAsync(AdvertDtoForInsertion advertDto);
    Task UpdateAdvertByIdAsync(int id, AdvertDtoForUpdate requestDto);
    Task DeleteAdvertByIdAsync(int id);
}