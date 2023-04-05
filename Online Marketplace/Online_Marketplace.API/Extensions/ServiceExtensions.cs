using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Online_Marketplace.BLL.Implementation.MarketServices;
using Online_Marketplace.BLL.Implementation.ProfileServices;
using Online_Marketplace.BLL.Implementation.Services;
using Online_Marketplace.BLL.Implementation.UserServices;
using Online_Marketplace.BLL.Interface.IMarketServices;
using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.BLL.Interface.IUserServices;

using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger;
using Online_Marketplace.Logger.Logger;
using System.Text;


namespace Online_Marketplace.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<MarketPlaceDBContext>(opts =>
        opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<MarketPlaceDBContext>()
            .AddDefaultTokenProviders();
        }
        public static void ConfigureAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<IBuyerServices, BuyerServices>();
            services.AddScoped<ISellerServices, SellerServices>();
            
            services.AddScoped<IAdminProfileServices, AdminProfileServices>();
            services.AddScoped<ISellerProfileServices, SellerProfileServices>();
            services.AddScoped<IBuyerProfileServices, BuyerProfileServices>();

            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IAdminProfileServices, AdminProfileServices>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

        }

    }
}
