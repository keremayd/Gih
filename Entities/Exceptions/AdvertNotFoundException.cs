namespace Entities.Exceptions;

public class AdvertNotFoundException : NotFoundException
{
    public AdvertNotFoundException(int? id) : base($"The advert with id: {id} could not found.")
    {
    }
}