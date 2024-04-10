using Entities;
using Services.DataTransferObjects.Request;

namespace Services.Contracts;

public interface IPersonService
{
    IQueryable<Person> GetPerson();
    Task<Person> GetPersonByIdAsync(int id);
    Task CreatePersonAsync(PersonDtoForInsertion personDto);
    Task UpdatePersonByIdAsync(int id, PersonDtoForUpdate personDto);
    Task DeletePersonByIdAsync(int id);
    Task<Person?> GetPersonByEmail(string email);
    Task<Person?> GetPersonByNickName(string nickName);
    Task<bool> UpdatePassword(string personEmail, string currentPassword, string newPassword);
}