namespace Services.DataTransferObjects.Request;

public class AdvertDtoForUpdate
{
    public string AdvertName { get; set; }
    public string AdvertDescription { get; set; } 
    public int AdvertKilo { get; set; }
    public DateTime? AdvertDate { get; set; }
}