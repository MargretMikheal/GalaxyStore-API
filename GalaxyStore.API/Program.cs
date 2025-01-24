using GalaxyStore.API.Extensions;

namespace GalaxyStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices(builder.Configuration);
            builder.Services.ConfigureAuthentication(builder.Configuration);
            builder.Services.ConfigureSwagger();

            var app = builder.Build();

            app.ConfigureMiddleware();

            app.Run();
        }
    }
}
