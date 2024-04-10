using Entities;
using Services.Contracts;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DataTransferObjects.Request;
using WebApi.ActionFilters;

namespace WebApi.Controllers;

[ServiceFilter(typeof(LogFilterAttribute))]
[ApiController]
[Route("api/adverts")]
public class AdvertsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AdvertsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetAllAdvert()
    {
        var adverts = _serviceManager.AdvertService.GetAdvert();
        return Ok(adverts);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateAdvert([FromBody] AdvertDtoForInsertion advertDto)
    {
        await _serviceManager.AdvertService.CreateAdvertAsync(advertDto);
        return StatusCode(201, advertDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAdvertById([FromRoute(Name = "id")] int advertId)
    {
        var advert = await _serviceManager.AdvertService.GetAdvertByIdAsync(advertId);
        return Ok(advert);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAdvertById(int id, AdvertDtoForUpdate advertDto)
    {
        await _serviceManager.AdvertService.UpdateAdvertByIdAsync(id, advertDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAdvertById(int id)
    {
        await _serviceManager.AdvertService.DeleteAdvertByIdAsync(id);
        return NoContent();
    }

    [HttpGet("{address}")]
    public async Task<IActionResult> GetAdvertByAddress(string address)
    {
        var advert = await _serviceManager.AdvertService.GetAdvertByAddressAsync(address);
        return Ok(advert);
    }
}