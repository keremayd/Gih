using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories;

public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository
{
    private readonly ApplicationDbContext _dbContext;
    public AdvertRepository(ApplicationDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public void CreateAdvert(Advert advert)
    {
       Create(advert);
    }  
    
    public void UpdateAdvert(Advert advert)
    {
        Update(advert);
    }

    public void DeleteAdvert(Advert advert)
    {
        Delete(advert);
    }

    public IQueryable<Advert> GetAdvert()
    {
        return FindAll();
    }

    public async Task<IEnumerable<Advert>> GetAdvertByAddressAsync(string address)
    {
        //adrese göre restorantIdsini aldık.
        var restaurantSameAddress = await _dbContext.Restaurants
            .Where(r => r.restaurantAdress == address)
            .ToListAsync();
        //RestorantId lerini listeye koyduk.
        var restaurantIds = restaurantSameAddress.Select(r => r.restaurantId).ToList();

        //İlanlardaki restorantIdleri karşılaştırıp aynı olanları listeye aldık.
        var adverts = await _dbContext.Adverts
            .Where(i => restaurantIds.Contains(i.RestaurantId))
            .ToListAsync();

        // Veritabanından alınan verileri dilimledik
        adverts = adverts.Where(i => restaurantIds.Contains(i.RestaurantId))
                         .ToList();

        return adverts;
    }
    public async Task<IEnumerable<Advert>> GetAdvertByRestaurantIdAsync(int id)
    {
        //adrese göre restorantIdsini aldık.
        var restaurantSameId = await _dbContext.Restaurants
            .Where(r => r.restaurantId == id)
            .ToListAsync();
        
        //RestorantId lerini listeye koyduk.
        var restaurantIds = restaurantSameId.Select(r => r.restaurantId).ToList();

        //İlanlardaki restorantIdleri karşılaştırıp aynı olanları listeye aldık.
        var adverts = await _dbContext.Adverts
            .Where(i => restaurantIds.Contains(i.RestaurantId))
            .ToListAsync();

        // Veritabanından alınan verileri dilimledik
        adverts = adverts.Where(i => restaurantIds.Contains(i.RestaurantId))
            .ToList();

        return adverts;
    }

    public async Task<Advert?> GetAdvertByIdAsync(int id)
    {
        return await FindByCondition(b => b.AdvertId.Equals(id)).SingleOrDefaultAsync();
    }

  
}