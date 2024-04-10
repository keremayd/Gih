using Entities;
using Services.DataTransferObjects.Request;

namespace Services.Contracts;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetRestaurant();
    Task<Restaurant?> GetRestaurantById(int id);
    Task<Restaurant> CreateRestaurant(RestaurantDtoForInsertion restaurantDto);
    Task UpdateRestaurantByIdAsync(int id, RestaurantDtoForUpdate restaurantDto);
    Task<Restaurant?> GetRestaurantByAddress(string address);
    Task DeleteRestaurantById(int id);
    Task<Restaurant?> GetRestaurantByEmail(string email);
    Task<Restaurant?> GetRestaurantByNickName(string nickName);
    Task<bool> UpdatePasswordAsync(string personEmail, string currentPassword, string newPassword);
}