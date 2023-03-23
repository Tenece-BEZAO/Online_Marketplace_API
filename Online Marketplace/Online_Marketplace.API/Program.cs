using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Online_Marketplace.API.Extensions;
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
            builder.Services.ConfigureAuthService();

            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();

            builder.Services.ConfigureJWT(builder.Configuration);

            builder.Services.ConfigureSqlContext(builder.Configuration);

            builder.Services.AddScoped<ValidationFilterAttribute>();

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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