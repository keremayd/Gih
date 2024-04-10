namespace Services.Contracts;

public interface IServiceManager
{
    IPersonService PersonService { get; }
    IRestaurantService RestaurantService { get; }
    IAdvertService AdvertService {get;}
    ICsoService CsoService {get;}
}