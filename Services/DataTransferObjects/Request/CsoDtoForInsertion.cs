using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.DataTransferObjects.Request;

public class CsoDtoForInsertion
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Number { get; set; }
    public int RoleId { get; set; }
    public string? PasswordSalt { get; set; }
}