using System.Security.Cryptography;
using System.Text;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class PersonValidateService : IPersonValidateService
{

    private readonly IRepositoryManager _manager;

    public PersonValidateService(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<bool> ValidatePerson(string username, string password)
    {
        
        var person = await _manager.PersonRepository.GetPersonByUsernameAsync(username);

        if (person == null)
        {
            return false;
        }

        var validUsername = person.PersonNickName;
        var validPassword = person.PersonPassword;
        var validPasswordSalt = person.PasswordSalt;

        if (validUsername != null && validPassword != null && validPasswordSalt != null)
        {
            var validVerifyPassword = PasswordHasher.VerifyPassword(password, validPassword, validPasswordSalt);

            if (username == validUsername && validVerifyPassword)
            {
                return true;
            }
        }

        return false;
    }
}