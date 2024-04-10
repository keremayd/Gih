using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.ActionFilters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        
        //Dto'yu çektik
        var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;
        
        //Dto boş geldiyse 
        if (param is null)
        {
            context.Result = new BadRequestObjectResult($"Dto is null. " + $"Controller : {controller}"); //400
            return;
        }
        
        //Geçersiz istek ise (örn. 1000tl nin üstünde fiyat)
        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState); //422
        }
    }
}