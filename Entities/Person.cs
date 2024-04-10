using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Entities;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public string PersonSurname { get; set; } 
    public string PersonEmail { get; set; }
    public string PersonPassword { get; set; }
    public string PersonNickName { get; set; }
    public string PersonPhoneNumber { get; set; }
    public int RoleId { get; set; }
    public string? PasswordSalt { get; set; }
}