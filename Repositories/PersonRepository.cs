using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories;

public class PersonRepository:RepositoryBase<Person>,IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }   
    public IQueryable<Person> GetPerson()
    {
        return FindAll();
    }
    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        return await FindByCondition(b => b.PersonId.Equals(id)).SingleOrDefaultAsync();
    }
    public async Task<Person?> GetPersonByEmailAsync(string email)
    {
        return await FindByCondition(b => b.PersonEmail.Equals(email)).SingleOrDefaultAsync();
    }

    public async Task<Person?> GetPersonByUsernameAsync(string username)
    {
        return await FindByCondition(b => b.PersonNickName.Equals(username)).SingleOrDefaultAsync();
    }

    public void CreatePerson(Person person)
    {
        Create(person);
    }

    public void UpdatePerson(Person person)
    {
        Update(person);
    }
    
    public void UpdatePersonPassword(Person person)
    {
        Update(person);
    }

    public void DeletePerson(Person person)
    {
        Delete(person);
    }
}