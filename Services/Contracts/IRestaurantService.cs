using Entities;
using Services.DataTransferObjects.Request;

namespace Services.Contracts;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetRestaurant();
    Task<IEnumerable<Restaurant>> GetRestaurantSortByScore();
    Task<Restaurant?> GetRestaurantById(int id);
    Task<Restaurant> CreateRestaurant(RestaurantDtoForInsertion restaurantDto);
    Task UpdateRestaurantByIdAsync(int id, RestaurantDtoForUpdate restaurantDto);
    Task UpdateScoreByIdAsync(int id);
    Task<Restaurant?> GetRestaurantByAddress(string address);
    Task DeleteRestaurantById(int id);
    Task<Restaurant?> GetRestaurantByEmail(string email);
    Task<Restaurant?> GetRestaurantByNickName(string nickName);
    Task<bool> UpdatePasswordAsync(string personEmail, string currentPassword, string newPassword);
}