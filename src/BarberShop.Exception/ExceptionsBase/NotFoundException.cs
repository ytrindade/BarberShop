using System.Net;

namespace BarberShop.Exception.ExceptionsBase;
public class NotFoundException : BarberShopException
{
    public NotFoundException(string error) : base(error)
    {
    }

    public override List<string> GetErrors() => [Message];

    public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.NotFound;
}
