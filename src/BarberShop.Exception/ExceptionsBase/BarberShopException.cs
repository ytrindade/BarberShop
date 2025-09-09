using System.Net;

namespace BarberShop.Exception.ExceptionsBase;
public abstract class BarberShopException : SystemException
{
    protected BarberShopException(string error) : base(error) {  }

    public abstract List<string> GetErrors();
    public abstract HttpStatusCode GetHttpStatusCode();
}
