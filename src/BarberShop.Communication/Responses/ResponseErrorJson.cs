namespace BarberShop.Communication.Responses;
public class ResponseErrorJson
{
    public List<string> Errors { get; private set; }

    public ResponseErrorJson(List<string> errors)
    {
        Errors = errors;
    }

    public ResponseErrorJson(string errors)
    {
        Errors = [errors];
    }
}
