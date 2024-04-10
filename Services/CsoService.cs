using AutoMapper;
using Entities;
using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;
using Services.DataTransferObjects.Request;

namespace Services;

public class CsoService : ICsoService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public CsoService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Cso>> GetCsoAsync()
    {
        return await _repositoryManager.Cso.GetCso();
    }

    public async Task<Cso?> GetCsoByIdAsync(int id)
    {
        var entity = await _repositoryManager.Cso.GetCsoByIdAsync(id);
        if (entity is null)
        {
            throw new CsoNotFoundException(id);
        }

        return entity;
    }

    public async Task<Cso> CreateCsoAsync(CsoDtoForInsertion csoDto)
    {
        var csoEmail = await _repositoryManager.Cso.GetCsoByEmail(csoDto.Email);
        var csoUsername = await _repositoryManager.Cso.GetCsoByUsername(csoDto.Username);
        if (csoEmail is not null || csoUsername is not null)
        {
            throw new InvalidOperationException("This email address or username is used.");
        }
        
        var (hashedPassword, salt) = PasswordHasher.HashPassword(csoDto.Password);
        
        var entity = _mapper.Map<Cso>(csoDto);
        entity.PasswordSalt = salt;
        entity.Password = hashedPassword;
        
        _repositoryManager.Cso.CreateCso(entity);
        await _repositoryManager.SaveAsync();

        return entity;
    }

    public async Task<Cso?> GetCsoByAddressAsync(string address)
    {
        var entity = await _repositoryManager.Cso.GetCsoByAddress(address);
        if (entity is null)
        {
            throw new CsoNotFoundException(null);
        }

        return entity;
    }

    public async Task DeleteCsoByIdAsync(int id)
    {
        var entity = await _repositoryManager.Cso.GetCsoByIdAsync(id);
        if (entity is null)
        {
            throw new CsoNotFoundException(id);
        }

        _repositoryManager.Cso.DeleteCso(entity);
        await _repositoryManager.SaveAsync();
    }

    public async Task<Cso?> GetCsoByEmailAsync(string email)
    {
        return await _repositoryManager.Cso.GetCsoByEmail(email);
    }

    public async Task<bool> UpdateCsoPasswordAsync(string email, string currentPassword, string newPassword)
    {
        var cso = await _repositoryManager.Cso.GetCsoByEmail(email);
        if (cso is null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (!PasswordHasher.VerifyPassword(currentPassword, cso.Password, cso.PasswordSalt))
        {
            throw new InvalidOperationException("Password could not be verified");
        }

        //Sha 256 ya göre yeni şifreyi şifreleyerek restorantı update et.
        var (newHashedPassword, newSalt) = PasswordHasher.HashPassword(newPassword);
        cso.Password = newHashedPassword;
        cso.PasswordSalt = newSalt;

        _repositoryManager.Cso.UpdateCsoPassword(cso);
        await _repositoryManager.SaveAsync();
        return true;
    }
}