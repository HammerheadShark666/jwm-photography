using FluentValidation;
using PhotographySite.Areas.Admin.Business;

namespace PhotographySite.StartUp
{
    public class SetUpFluentValidation
    {
        public static void Setup(WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<GalleryValidator>();
        }
    }
}
