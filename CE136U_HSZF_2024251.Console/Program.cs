
using CE136U_HSZF_2024251.Application;
using CE136U_HSZF_2024251.Application.CE136U_HSZF_2024251.Application; // Fasz se tudja miért működik de igen
using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CE136U_HSZF_2024251.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register DbContext and other services
                    services.AddDbContext<TheWitchAppDataBaseContext>();
                    services.AddSingleton<IAttributesDataProvider, AttributesDataProvider>();
                    services.AddSingleton<IHeroesDataProvider, HeroDataProvider>();
                    services.AddSingleton<IMonsterDataProvider, MonsterDataProvider>();
                    services.AddSingleton<IResourcesDataProvider, ResourcesDataProvider>();
                    services.AddSingleton<ITasksProvider, TasksDataProvider>();
                    services.AddSingleton<IAffectedSatues,AffectedSatuesDataProvider>();

                    // Register Services
                    services.AddScoped<IAffectedSatuesService,AffectedSatuesService>(); 
                    services.AddScoped<IAttributesService, AttributesService>();
                    services.AddScoped<IHeroService, HeroService>();
                    services.AddScoped<IMonsterService, MonsterService>();
                    services.AddScoped<IResourcesService, ResourcesServices>();
                    services.AddScoped<ITasksService, TaskService>();

                    // Register JsonDeserializer
                    services.AddScoped<JsonDeserializer>(); // Ensure it's scoped if it uses other scoped services
                })
                .Build();

            host.Start();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                // Resolve the JsonDeserializer service
                var jsonLoader = serviceProvider.GetRequiredService<JsonDeserializer>();

                // Call the DeserializeJson method with the path to your JSON file
                jsonLoader.DeserializeJson("Data.json");
            }
        }
    }
}