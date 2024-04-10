using Entities;

namespace Repositories.Contracts;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurant();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
    Task<Restaurant?> GetRestaurantByEmail(string email);
    Task<Restaurant?> GetRestaurantByAddress(string address);
    Task<Restaurant?> GetRestaurantByNickName(string nickname);
    void CreateRestaurant(Restaurant restaurant);
    void UpdateRestaurant(Restaurant restaurant);
    void UpdateRestaurantPassword(Restaurant restaurant);
    void DeleteRestaurant(Restaurant restaurant);
}