using GalaxyStore.API.Extensions;
using GalaxyStore.Data.seedingData;

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

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    DataSeeder.SeedRolesAndUsersAsync(services).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            app.Run();
        }
    }
}
