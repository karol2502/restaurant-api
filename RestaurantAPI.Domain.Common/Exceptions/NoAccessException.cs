namespace RestaurantAPI.Domain.Common.Exceptions;

public class NoAccessException : Exception
{
    public NoAccessException() : base("No access")
    {

    }
}