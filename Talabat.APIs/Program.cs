
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        // Entry Point
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Custom Services

            // DbContext Service
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add generic repository service
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

            #endregion


            var app = builder.Build();

            #region Auto Updating Database For Any Migration 

            using var scope = app.Services.CreateScope(); // new scope

            var services = scope.ServiceProvider;         // All Scoped services

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>(); // Ask ClR Explicity 

                await dbContext.Database.MigrateAsync();            // Apply Migrations 

                await StoreContextSeed.SeedAsync(dbContext);
            }

            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "An Error Occured During Apply The Migration");
            }


            #endregion

            #region Configure Kestral Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}