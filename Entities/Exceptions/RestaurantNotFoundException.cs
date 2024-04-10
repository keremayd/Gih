namespace Entities.Exceptions;

public class RestaurantNotFoundException : NotFoundException
{
    public RestaurantNotFoundException(int? id) : base($"The restaurant with id: {id} could not found.")
    {
    }
}