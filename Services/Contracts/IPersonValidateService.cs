namespace Services.Contracts;

public interface IPersonValidateService
{
    Task<bool> ValidatePerson(string username, string password);
}