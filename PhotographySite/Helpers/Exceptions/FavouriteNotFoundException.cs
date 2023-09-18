namespace SwanSong.Service.Helpers.Exceptions;

public class FavouriteNotFoundException : Exception
{
    public FavouriteNotFoundException()
    {
    }

    public FavouriteNotFoundException(string message)
        : base(message)
    {
    }

    public FavouriteNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}