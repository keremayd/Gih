using Services.DataTransferObjects.Request;

namespace Services.Contracts;

public interface IRestaurantValidateService
{
    Task<bool> ValidateRestaurant(DtoForAuth request);
}