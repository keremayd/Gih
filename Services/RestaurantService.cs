using AutoMapper;
using Entities;
using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;
using Services.DataTransferObjects.Request;

namespace Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    
    public RestaurantService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurant()
    {
       return await _repositoryManager.Restaurant.GetRestaurant();
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurantSortByScore()
    {
        return await _repositoryManager.Restaurant.GetRestaurantSortByScore();
    }
    
    public async Task<Restaurant?> GetRestaurantById(int id)
    {
        var entity = await _repositoryManager.Restaurant.GetRestaurantByIdAsync(id);
        if (entity is null)
        {
            throw new RestaurantNotFoundException(id);
        }

        return entity;
    }
    
    public async Task<Restaurant?> GetRestaurantByEmail(string email)
    {
        return await _repositoryManager.Restaurant.GetRestaurantByEmail(email);
    }
    
    public async Task<Restaurant?> GetRestaurantByAddress(string address)
    {
        var entity = await _repositoryManager.Restaurant.GetRestaurantByAddress(address);
        if (entity is null)
        {
            throw new RestaurantNotFoundException(null);
        }

        return entity;
    }

    public async Task<Restaurant?> GetRestaurantByNickName(string nickName)
    {
        return await _repositoryManager.Restaurant.GetRestaurantByNickName(nickName);
    }
    
    public async Task<Restaurant> CreateRestaurant(RestaurantDtoForInsertion restaurantDto)
    {
        var restaurantEmail = await _repositoryManager.Restaurant.GetRestaurantByEmail(restaurantDto.restaurantMail);
        var restaurantNickname = await _repositoryManager.Restaurant.GetRestaurantByNickName(restaurantDto.restaurantNickname);
        if (restaurantEmail is not null || restaurantNickname is not null)
        {
            throw new InvalidOperationException("This e-mail address or username is used.");
        }
        
        var (hashedPassword, salt) = PasswordHasher.HashPassword(restaurantDto.restaurantPassword);
        
        var entity = _mapper.Map<Restaurant>(restaurantDto);
        entity.PasswordSalt = salt;
        entity.restaurantPassword = hashedPassword;
        
        _repositoryManager.Restaurant.CreateRestaurant(entity);
        await _repositoryManager.SaveAsync();

        return entity;
    }

    public async Task UpdateScoreByIdAsync(int id)
    {
        var entity = await _repositoryManager.Restaurant.GetRestaurantByIdAsync(id);
        if (entity is null)
        {
            throw new RestaurantNotFoundException(id);
        }
        entity.Score = entity.Score + 1;

        _repositoryManager.Restaurant.UpdateRestaurant(entity);
        await _repositoryManager.SaveAsync();
    }
    
    public async Task UpdateRestaurantByIdAsync(int id, RestaurantDtoForUpdate restaurantDto)
    {
        var entity = await _repositoryManager.Restaurant.GetRestaurantByIdAsync(id);
        if (entity is null)
        {
            throw new RestaurantNotFoundException(id);
        }

        _mapper.Map(restaurantDto, entity);

        _repositoryManager.Restaurant.UpdateRestaurant(entity);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteRestaurantById(int id)
    {
        var entity = await _repositoryManager.Restaurant.GetRestaurantByIdAsync(id);
        if (entity is null)
        {
            throw new RestaurantNotFoundException(id);
        }
        
        _repositoryManager.Restaurant.DeleteRestaurant(entity);
        await _repositoryManager.SaveAsync();
    }
    
    public async Task<bool> UpdatePasswordAsync(string email, string currentPassword, string newPassword)
    {
        var restaurant = await _repositoryManager.Restaurant.GetRestaurantByEmail(email);
        if (restaurant is null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (!PasswordHasher.VerifyPassword(currentPassword, restaurant.restaurantPassword,restaurant.PasswordSalt))
        {
            throw new InvalidOperationException("Password could not be verified");
            
        }
        //Sha 256 ya göre yeni şifreyi şifreleyerek restorantı update et.
        var (newHashedPassword, newSalt) = PasswordHasher.HashPassword(newPassword);
        restaurant.restaurantPassword = newHashedPassword;
        restaurant.PasswordSalt = newSalt;
         
        _repositoryManager.Restaurant.UpdateRestaurantPassword(restaurant);
        await _repositoryManager.SaveAsync();
        return true;
    }
}