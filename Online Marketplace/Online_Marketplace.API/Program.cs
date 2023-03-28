using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using NLog;
using Online_Marketplace.API.Extensions;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.Filters;
using System.Reflection;


namespace Online_Marketplace.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();

            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureAuthServices();

            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();

            builder.Services.ConfigureJWT(builder.Configuration);

            builder.Services.ConfigureSqlContext(builder.Configuration);

            builder.Services.AddScoped<ValidationFilterAttribute>();

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarketPlaceApp", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                            Array.Empty<string>()
                    },
                });
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<MarketPlaceDBContext>>();

            builder.Services.ConfigureServices();
/*            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("jwt"));
*/          builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(Assembly.Load("Online_Marketplace.BLL"));


            var app = builder.Build();

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}