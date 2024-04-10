using Entities;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.DataTransferObjects.Request;
using WebApi.ActionFilters;

namespace WebApi.Controllers;

//Civil society organization
[ApiController]
[Route("api/csos")]
public class CsosController : ControllerBase
{
    private readonly IServiceManager _manager;
    private readonly IAuthenticationService _authService;
    private readonly IPersonValidateService _personValidateService;

    public CsosController(IServiceManager manager, IPersonValidateService personValidateService,
        IAuthenticationService authService)
    {
        _manager = manager;
        _personValidateService = personValidateService;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPerson()
    {
        var products = await _manager.CsoService.GetCsoAsync();
        return Ok(products);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPersonById(int id)
    {
        var products = await _manager.CsoService.GetCsoByIdAsync(id);
        return Ok(products);
    }
    
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateCso([FromBody] CsoDtoForInsertion csoDto)
    {
        await _manager.CsoService.CreateCsoAsync(csoDto);
        return Ok(csoDto);
    }
}