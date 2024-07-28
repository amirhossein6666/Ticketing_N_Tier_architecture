namespace Ticketing.businessLogicLayer.Tools.CustomExceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string message) : base(message) { }
}