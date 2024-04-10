namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IPersonRepository PersonRepository { get; }
    IRestaurantRepository Restaurant { get; }
    IAdvertRepository AdvertRepository {get;}
    ICsoRepository Cso {get;}
    Task SaveAsync();
}