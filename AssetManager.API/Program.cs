using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context;
using AssetManager.API.Context.Models;
using AssetManager.API.Context.Repository;
using AssetManager.API.Service;
using AssetManager.API.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AssetManagerContext>(option =>
            {
                var connectionString = builder.Configuration.GetConnectionString("AssetManagerConnection");

                option.UseSqlite(connectionString);
            });

            builder.Services.AddUnitOfWork<AssetManagerContext>();
            builder.Services.AddCustomRepository<Project, ProjectItemRepository>();
            builder.Services.AddCustomRepository<AssetPackage, ResourcePackageRepository>();
            builder.Services.AddCustomRepository<Platform, PlatformRepository>();

            builder.Services.AddTransient<IProjectItemService, ProjectItemService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
