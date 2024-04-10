using Entities;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.DataTransferObjects.Request;
using WebApi.ActionFilters;

namespace WebApi.Controllers;

[ApiController]
[Route("api/persons")]
public class PersonsController : ControllerBase
{
    private readonly IServiceManager _manager;
    private readonly IAuthenticationService _authService;
    private readonly IPersonValidateService _personValidateService;

    public PersonsController(IServiceManager manager, IPersonValidateService personValidateService,
        IAuthenticationService authService)
    {
        _manager = manager;
        _personValidateService = personValidateService;
        _authService = authService;
    }

    [HttpGet]
    public IActionResult GetPerson()
    {
        var products = _manager.PersonService.GetPerson();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPersonById(int id)
    {
        var products = await _manager.PersonService.GetPersonByIdAsync(id);
        return Ok(products);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonDtoForInsertion personDto)
    {
        await _manager.PersonService.CreatePersonAsync(personDto);
        return Ok(personDto);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePersonById(int id, PersonDtoForUpdate personDto)
    {
        await _manager.PersonService.UpdatePersonByIdAsync(id, personDto);
        return NoContent();
    }

    [HttpPut("password")]
    public async Task<IActionResult> UpdatePersonByEmail(string email, string currentPassword, string newPassword)
    {
        try
        {
            // Kullanıcı şifresini güncelle
            bool passwordUpdated = await _manager.PersonService.UpdatePassword(email, currentPassword, newPassword);

            if (passwordUpdated)
            {
                return Ok("Şifre başarıyla güncellendi.");
            }
            else
            {
                return BadRequest("Mevcut şifre doğrulanamadı veya kullanıcı bulunamadı.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePersonById(int id)
    {
        await _manager.PersonService.DeletePersonByIdAsync(id);
        return NoContent();
    }

    //[Authorize] tokenli girilen yerlere koy
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost("login")]
    public async Task<IActionResult> LoginPerson(DtoForAuth dto)
    {
        try
        {
            if (await _personValidateService.ValidatePerson(dto.Username, dto.Password))
            {
                var token = _authService.GenerateJwtToken(dto.Username);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}