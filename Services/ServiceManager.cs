using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IPersonService> _personService;
    private readonly Lazy<IRestaurantService> _restaurantService;
    private readonly Lazy <IAdvertService> _advertService;
    private readonly Lazy <ICsoService> _csoService;
    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _personService = new Lazy<IPersonService>(() => new PersonService(repositoryManager, mapper));
        _restaurantService = new Lazy<IRestaurantService>(() => new RestaurantService(repositoryManager, mapper));
        _advertService = new Lazy<IAdvertService>(()=> new AdvertService(repositoryManager, mapper));
        _csoService = new Lazy<ICsoService>(()=> new CsoService(repositoryManager, mapper));
    }

    public IPersonService PersonService => _personService.Value;
    public IRestaurantService RestaurantService => _restaurantService.Value;
    public IAdvertService AdvertService => _advertService.Value;
    public ICsoService CsoService => _csoService.Value;
}