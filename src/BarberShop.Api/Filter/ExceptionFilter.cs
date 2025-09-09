using BarberShop.Communication.Responses;
using BarberShop.Exception;
using BarberShop.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;

namespace BarberShop.Api.Filter;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is BarberShopException barberShopException)
        {
            context.HttpContext.Response.StatusCode = (int)barberShopException.GetHttpStatusCode();

            context.Result = new ObjectResult(new ResponseErrorJson(barberShopException.GetErrors()));
        }
        else
            ThrowUnknownError(context);
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        context.Result = new ObjectResult(new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR));
    }
}
