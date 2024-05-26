namespace iTicket.Application.Bases
{
    public abstract class BaseNotFoundException(string message) : Exception(message)
    {
    }
}
