using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories;

public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurant()
    {
        return await FindAll().ToListAsync();
    }
    
    public async Task<IEnumerable<Restaurant>> GetRestaurantSortByScore()
    {
        return await FindAllSortByScore().ToListAsync();
    }
    
    public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
    {
        return await FindByCondition(b => b.restaurantId == id).SingleOrDefaultAsync(b => b.restaurantId == id);
    }

    public async Task<Restaurant?> GetRestaurantByEmail(string email)
    {
        return await FindByCondition(b => b.restaurantMail.Equals(email)).SingleOrDefaultAsync();
    }

    public async Task<Restaurant?> GetRestaurantByAddress(string address) =>
        await FindByCondition(b => b.restaurantAdress.Equals(address)).SingleOrDefaultAsync();
    
    public async Task<Restaurant?> GetRestaurantByNickName(string nickname)
    {
        return await FindByCondition(b => b.restaurantNickname.Equals(nickname)).FirstOrDefaultAsync();
    }

    public void CreateRestaurant(Restaurant restaurant)
    {
        Create(restaurant);
    }

    public void UpdateRestaurant(Restaurant restaurant)
    {
        Update(restaurant);
    }

    public void UpdateRestaurantPassword(Restaurant restaurant)
    {
        Update(restaurant);
    }

    public void DeleteRestaurant(Restaurant restaurant)
    {
        Delete(restaurant);
    }
}