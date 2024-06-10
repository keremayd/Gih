using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int restaurantId { get; set; }
    public string restaurantName { get; set; }
    public string restaurantAdress { get; set; }
    public string restaurantNickname { get; set; }
    public string restaurantMail { get; set; }
    public string restaurantPassword { get; set; }
    public string restaurantNumber { get; set; }
    public int RoleId { get; set; }
    public string? PasswordSalt { get; set; }
    public int Score { get; set; }
}