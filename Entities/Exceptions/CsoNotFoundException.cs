namespace Entities.Exceptions;

public class CsoNotFoundException : NotFoundException
{
    public CsoNotFoundException(int? id) : base($"The Civil Society Organization with id: {id} could not found.")
    {
    }
}