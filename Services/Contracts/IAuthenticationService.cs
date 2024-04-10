namespace Services.Contracts;

public interface IAuthenticationService
{
    string GenerateJwtToken(string username);
}