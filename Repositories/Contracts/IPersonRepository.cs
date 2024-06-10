using Entities;

namespace Repositories.Contracts;

public interface IPersonRepository
{
    IQueryable<Person> GetPerson();
    IQueryable<Person> GetPersonSortByScore();
    Task<Person?> GetPersonByIdAsync(int id);
    Task<Person?> GetPersonByEmailAsync(string email);
    Task<Person?> GetPersonByUsernameAsync(string username);
    void CreatePerson(Person person);
    void UpdatePerson(Person person);
    void UpdatePersonPassword(Person person);
    void DeletePerson(Person person);
}