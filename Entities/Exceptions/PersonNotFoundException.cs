namespace Entities.Exceptions;

public class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(int? id) : base($"The person with id: {id} could not found.")
    {
    }
}