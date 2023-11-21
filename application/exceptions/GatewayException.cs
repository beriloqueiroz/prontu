namespace application;
public class GatewayException : Exception
{
    public GatewayException()
    {
    }

    public GatewayException(string message)
        : base(message)
    {
    }

    public GatewayException(string message, Exception inner)
        : base(message, inner)
    {
    }
}