using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;
using Services.DataTransferObjects.Request;

namespace Services;

public class RestaurantValidateService : IRestaurantValidateService
{
    private readonly IRepositoryManager _repositoryManager;

    public RestaurantValidateService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<bool> ValidateRestaurant(DtoForAuth request)
    {
        var restaurant = await _repositoryManager.Restaurant.GetRestaurantByNickName(request.Username);
        
        if (restaurant == null)
        {
            throw new RestaurantNotFoundException(null);
        }

        var validUsername = restaurant.restaurantNickname;
        var validPassword = restaurant.restaurantPassword;
        var validPasswordSalt = restaurant.PasswordSalt;
        
        if (validUsername != null && validPassword != null && validPasswordSalt != null)
        {
            var validVerifyPassword = PasswordHasher.VerifyPassword(request.Password, validPassword, validPasswordSalt);

            if (request.Username == validUsername && validVerifyPassword)
            {
                return true;
            }
        }

        return false;
    }
}