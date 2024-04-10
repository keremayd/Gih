using AutoMapper;
using Entities;
using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;
using Services.DataTransferObjects.Request;

namespace Services;

public class AdvertService : IAdvertService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public AdvertService(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public IQueryable<Advert> GetAdvert()
    {
        return _manager.AdvertRepository.GetAdvert();
    }

    public async Task<Advert?> GetAdvertByIdAsync(int id)
    {
        var entity = await _manager.AdvertRepository.GetAdvertByIdAsync(id);
        if (entity is null)
        {
            throw new AdvertNotFoundException(id);
        }

        return entity;
    }

    public async Task<IEnumerable<Advert>> GetAdvertByAddressAsync(string address)
    {
        var entity = await _manager.AdvertRepository.GetAdvertByAddressAsync(address);
        if (entity is null)
        {
            throw new AdvertNotFoundException(null);
        }

        return entity;
    }

    public async Task<IEnumerable<Advert>> GetAdvertByRestaurantIdAsync(int id)
    {
        var entity = await _manager.AdvertRepository.GetAdvertByRestaurantIdAsync(id);
        if (entity is null)
        {
            throw new AdvertNotFoundException(id);
        }

        return entity;
    }

    public async Task CreateAdvertAsync(AdvertDtoForInsertion advertDto)
    {
        var entity = _mapper.Map<Advert>(advertDto);
        _manager.AdvertRepository.CreateAdvert(entity);
        await _manager.SaveAsync();
    }

    public async Task UpdateAdvertByIdAsync(int id, AdvertDtoForUpdate requestDto)
    {
        var entity = await _manager.AdvertRepository.GetAdvertByIdAsync(id);
        if (entity is null)
        {
            throw new AdvertNotFoundException(id);
        }

        _mapper.Map(requestDto, entity);
        entity.AdvertDate = DateTime.UtcNow;
        
        _manager.AdvertRepository.UpdateAdvert(entity);
        await _manager.SaveAsync();
    }

    public async Task DeleteAdvertByIdAsync(int id)
    {
        var entity = await _manager.AdvertRepository.GetAdvertByIdAsync(id);
        if (entity is null)
        {
            throw new AdvertNotFoundException(id);
        }

        _manager.AdvertRepository.DeleteAdvert(entity);
        await _manager.SaveAsync();
    }
}