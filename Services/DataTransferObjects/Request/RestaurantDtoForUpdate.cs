namespace Services.DataTransferObjects.Request;

public class RestaurantDtoForUpdate
{
    public int restaurantId { get; set; }
    public string restaurantName { get; set; }
    public string restaurantAdress { get; set; } 
    public string restaurantPhoneNumber { get; set; }
}