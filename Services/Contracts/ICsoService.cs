using Entities;
using Services.DataTransferObjects.Request;

namespace Services.Contracts;

public interface ICsoService
{
    Task<IEnumerable<Cso>> GetCsoAsync();
    Task<Cso?> GetCsoByIdAsync(int id);
    Task<Cso> CreateCsoAsync(CsoDtoForInsertion csoDto);
    //Task UpdateCsoByIdAsync(int id, RestaurantDtoForUpdate restaurantDto);
    Task<Cso?> GetCsoByAddressAsync(string address);
    Task DeleteCsoByIdAsync(int id);
    Task<Cso?> GetCsoByEmailAsync(string email);
    Task<bool> UpdateCsoPasswordAsync(string personEmail, string currentPassword, string newPassword);
}