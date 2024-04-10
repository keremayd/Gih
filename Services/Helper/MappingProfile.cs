using AutoMapper;
using Entities;
using Services.DataTransferObjects.Request;

namespace Services.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Advert
        CreateMap<AdvertDtoForInsertion, Advert>();
        CreateMap<AdvertDtoForUpdate, Advert>();
        
        //Person
        CreateMap<PersonDtoForInsertion, Person>();
        CreateMap<PersonDtoForUpdate, Person>();
        
        //Restaurant
        CreateMap<RestaurantDtoForInsertion, Restaurant>();
        CreateMap<RestaurantDtoForUpdate, Restaurant>();
        
        //Cso
        CreateMap<CsoDtoForInsertion, Cso>();
    }
}