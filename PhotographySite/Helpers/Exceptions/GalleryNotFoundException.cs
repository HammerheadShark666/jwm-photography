namespace SwanSong.Service.Helpers.Exceptions;

public class GalleryNotFoundException : Exception
{
    public GalleryNotFoundException()
    {
    }

    public GalleryNotFoundException(string message)
        : base(message)
    {
    }

    public GalleryNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}