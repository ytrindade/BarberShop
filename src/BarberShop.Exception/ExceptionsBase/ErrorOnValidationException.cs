using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Exception.ExceptionsBase;
public class ErrorOnValidationException : BarberShopException
{
    private readonly List<string> _errors;

    public ErrorOnValidationException(List<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }
    public override List<string> GetErrors() => _errors;

    public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
}
