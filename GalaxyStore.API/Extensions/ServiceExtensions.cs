using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Core.Service;
using GalaxyStore.Core.Service.Mappings;
using GalaxyStore.Core.Services;
using GalaxyStore.Data;
using GalaxyStore.Data.Repositories;
using GalaxyStore.Data.Repository;
using GalaxyStore.Domain.Helper;
using GalaxyStore.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GalaxyStore.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            MapsterConfiguration.RegisterMappings();
            services.Configure<JWT>(configuration.GetSection("JWT"));
            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configure DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CS")));

            services.AddEndpointsApiExplorer();
        }
    }
}
