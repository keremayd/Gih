using Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.DataTransferObjects.Request;
using WebApi.ActionFilters;

namespace WebApi.Controllers;

//TODO ValidationFilter Ekle
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly IAuthenticationService _authService;
    private readonly IRestaurantValidateService _personValidateService;

    public RestaurantsController(IServiceManager serviceManager, IRestaurantValidateService personValidateService,
        IAuthenticationService authService)
    {
        _serviceManager = serviceManager;
        _personValidateService = personValidateService;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRestaurant()
    {
        var restaurant = await _serviceManager.RestaurantService.GetRestaurant();
        return Ok(restaurant);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDtoForInsertion restaurantDto)
    {
        try
        {
            var response = await _serviceManager.RestaurantService.CreateRestaurant(restaurantDto);
            return StatusCode(201, response);
        }
        catch
        {
            return BadRequest("This e-mail address or username is used");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _serviceManager.RestaurantService.GetRestaurantById(id);
        return Ok(restaurant);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRestaurantById(int id, RestaurantDtoForUpdate restaurantDto)
    {
        await _serviceManager.RestaurantService.UpdateRestaurantByIdAsync(id, restaurantDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRestaurantById(int id)
    {
        await _serviceManager.RestaurantService.DeleteRestaurantById(id);
        return NoContent();
    }

    [HttpGet("{address}")]
    public async Task<IActionResult> GetRestaurantByAddress(string address)
    {
        var restaurant = await _serviceManager.RestaurantService.GetRestaurantByAddress(address);
        return Ok(restaurant);
    }

    [HttpPut("password")]
    public async Task<IActionResult> UpdateRestaurantByEmail(string email, string currentPassword, string newPassword)
    {
        try
        {
            bool passwordUpdated =
                await _serviceManager.RestaurantService.UpdatePasswordAsync(email, currentPassword, newPassword);

            if (passwordUpdated)
            {
                return Ok("Password updated successfully");
            }
            else
            {
                return BadRequest("The current password was not verified or the user was not found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Something went wrong: {ex.Message}");
        }
    }

    [HttpGet("{id:int}/adverts")]
    public async Task<IActionResult> GetAdvertByRestaurantId(int id)
    {
        var advert = await _serviceManager.AdvertService.GetAdvertByRestaurantIdAsync(id);
        return Ok(advert);
    }
    
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost("login")]
    public async Task<IActionResult> LoginRestaurant(DtoForAuth restaurantDto)
    {
        var checkPerson =
            await _personValidateService.ValidateRestaurant(restaurantDto);

        if (checkPerson)
        {
            // Kullanıcı doğrulama başarılı, JWT token oluşturulur.
            var token = _authService.GenerateJwtToken(restaurantDto.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}