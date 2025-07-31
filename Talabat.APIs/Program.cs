using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Threading.Tasks;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helper;
using Talabat.APIs.Middlewares;
using TalabatProject.Core.Entity.Identity;
using TalabatProject.Core.Interfaces;
using TalabatProject.Core.Repositories;
using TalabatProject.Repository;
using TalabatProject.Repository.Basket;
using TalabatProject.Repository.Data;
using TalabatProject.Repository.Identity;
using TalabatProject.Repository.Identity.DataSeed;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {       
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services With Work DI
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddSwaggerServices();

            builder.Services.AddApplicationServices();

            #endregion

            var app = builder.Build();

            var scoped = app.Services.CreateScope(); // create new scope for DI
            var services = scoped.ServiceProvider; // get service provider from scope
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // get logger factory from service provider  , save errors
            try
            {
                var StoreDbContext = services.GetRequiredService<StoreContext>();
                await StoreDbContext.Database.MigrateAsync(); // Update Database
                await StoreContextSeed.StoreAsync(StoreDbContext);

                var AppIdentityDbContext = services.GetRequiredService<AppIdentityContext>();
                await AppIdentityDbContext.Database.MigrateAsync();

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var appIdentityContext = services.GetRequiredService<AppIdentityContext>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManager, appIdentityContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }

            #region Configure Piplines 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseMiddleware<RateLimitingMiddleware>();

            app.UseMiddleware<ExceptionErrorMiddleware>();

            app.UseMiddleware<ProfilingMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
