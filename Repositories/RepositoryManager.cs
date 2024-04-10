using Repositories.Contracts;

namespace Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private readonly Lazy<IPersonRepository> _personRepository;
    private readonly Lazy<IRestaurantRepository> _restaurantRepository;
    private readonly Lazy<IAdvertRepository> _advertRepository;
    private readonly Lazy<ICsoRepository> _csoRepository;
    
    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        _personRepository = new Lazy<IPersonRepository>(()=> new PersonRepository(_context));
        _restaurantRepository = new Lazy<IRestaurantRepository>(()=> new RestaurantRepository(_context));
        _advertRepository = new Lazy<IAdvertRepository>(()=> new AdvertRepository(_context));
        _csoRepository = new Lazy<ICsoRepository>(()=> new CsoRepository(_context));;
    }

    public IPersonRepository PersonRepository => _personRepository.Value;
    public IRestaurantRepository Restaurant => _restaurantRepository.Value;
    public IAdvertRepository AdvertRepository => _advertRepository.Value;
    public ICsoRepository Cso => _csoRepository.Value;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}