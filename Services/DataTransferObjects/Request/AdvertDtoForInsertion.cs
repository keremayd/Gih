namespace Services.DataTransferObjects.Request;

public class AdvertDtoForInsertion
{
    public int AdvertId {get; set;}
    public string AdvertName {get; set;}
    public int AdvertKilo {get; set;}
    public string AdvertDescription {get;set;}
    public DateTime? AdvertDate {get;set;}
    public int RestaurantId {get;set;}
}