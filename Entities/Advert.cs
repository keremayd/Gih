using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace Entities;

public class Advert
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AdvertId {get; set;}
    public string AdvertName {get; set;}
    public int AdvertKilo {get; set;}
    public string AdvertDescription {get;set;}
    public DateTime? AdvertDate {get;set;}
    public int RestaurantId {get;set;}
}