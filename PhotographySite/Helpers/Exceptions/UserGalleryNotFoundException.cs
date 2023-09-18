using System;

namespace SwanSong.Service.Helpers.Exceptions;

public class UserGalleryNotFoundException : Exception
{
    public UserGalleryNotFoundException()
    {
    }

    public UserGalleryNotFoundException(string message)
        : base(message)
    {
    }

    public UserGalleryNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}