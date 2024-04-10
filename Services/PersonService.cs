using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Entities;
using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;
using Services.DataTransferObjects.Request;

namespace Services;

public class PersonService : IPersonService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public PersonService(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public IQueryable<Person> GetPerson()
    {
        return _manager.PersonRepository.GetPerson();
    }

    public async Task<Person> GetPersonByIdAsync(int id)
    {
        var entity = await _manager.PersonRepository.GetPersonByIdAsync(id);
        if (entity is null)
        {
            throw new PersonNotFoundException(id);
        }

        return entity;
    }

    public async Task<Person?> GetPersonByEmail(string email)
    {
        return await _manager.PersonRepository.GetPersonByEmailAsync(email);
    }

    public async Task<Person?> GetPersonByNickName(string nickName)
    {
        return await _manager.PersonRepository.GetPersonByUsernameAsync(nickName);
    }

    public async Task<bool> UpdatePassword(string email, string currentPassword, string newPassword)
    {
        var person = await _manager.PersonRepository.GetPersonByEmailAsync(email);
        if (person is null)
        {
            throw new InvalidOperationException("Böyle bir kullanıcı yok");
        }

        if (!PasswordHasher.VerifyPassword(currentPassword, person.PersonPassword, person.PasswordSalt))
        {
            throw new InvalidOperationException("Şifre doğrulanamadı");
        }

        // Yeni şifreyi şifrele ve güncelle
        var (newHashedPassword, newSalt) = PasswordHasher.HashPassword(newPassword);
        person.PersonPassword = newHashedPassword;
        person.PasswordSalt = newSalt;

        _manager.PersonRepository.UpdatePersonPassword(person);
        await _manager.SaveAsync();
        return true;
    }

    public async Task CreatePersonAsync(PersonDtoForInsertion personDto)
    {
        var personEmail = await _manager.PersonRepository.GetPersonByEmailAsync(personDto.PersonEmail);
        var personNickName = await _manager.PersonRepository.GetPersonByUsernameAsync(personDto.PersonNickName);
        if (personEmail is not null || personNickName is not null)
        {
            throw new InvalidOperationException("Bu email adresi veya kullanıcı adı kullanılmaktadır.");
        }

        var (hashedPassword, saltPassword) = PasswordHasher.HashPassword(personDto.PersonPassword);

        var entity = _mapper.Map<Person>(personDto);
        entity.PersonPassword = hashedPassword;
        entity.PasswordSalt = saltPassword;
        // var newPerson = new Person
        // {
        //     PersonName = personDto.PersonName, 
        //     PersonSurname = personDto.PersonSurname,
        //     PersonEmail = personDto.PersonEmail,
        //     PersonPassword = hashedPassword,
        //     PersonPhoneNumber = personDto.PersonPhoneNumber,
        //     PersonNickName = personDto.PersonNickName,
        //     RoleId = personDto.RoleId,
        //     PasswordSalt = salt
        // };

        _manager.PersonRepository.CreatePerson(entity);
        await _manager.SaveAsync();
    }

    public async Task UpdatePersonByIdAsync(int id, PersonDtoForUpdate personDto)
    {
        var entity = await _manager.PersonRepository.GetPersonByIdAsync(id);
        if (entity is null)
        {
            throw new PersonNotFoundException(id);
        }

        // //entity.PersonId = person.PersonId;
        // entity.PersonName = person.PersonName;
        // entity.PersonSurname = person.PersonSurname;
        // entity.PersonPhoneNumber = person.PersonPhoneNumber;
        _mapper.Map(personDto, entity);

        _manager.PersonRepository.UpdatePerson(entity);
        await _manager.SaveAsync();
    }

    public async Task DeletePersonByIdAsync(int id)
    {
        var entity = await _manager.PersonRepository.GetPersonByIdAsync(id);
        if (entity is null)
        {
            throw new PersonNotFoundException(id);
        }

        _manager.PersonRepository.DeletePerson(entity);
        await _manager.SaveAsync();
    }
}