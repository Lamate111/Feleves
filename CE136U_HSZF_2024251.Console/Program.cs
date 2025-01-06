
using CE136U_HSZF_2024251.Application;
using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using WitcherSurvival;

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
                    services.AddSingleton<IAffectedStatuesDataProvider, AffectedStatuesDataProvider>();

                    // Register Services
                    
                    services.AddSingleton<IAttributesService, AttributesService>();
                    services.AddSingleton<IHeroService, HeroService>();
                    services.AddSingleton<IMonsterService, MonsterService>();
                    services.AddSingleton<IResourcesService, ResourcesServices>();
                    services.AddSingleton<ITasksService, TaskService>();
                    services.AddSingleton<IAffectedStatuesService, AffectedStatuesService>();

                    // Register JsonDeserializer
                    services.AddSingleton<JsonDeserializer>(); // Ensure it's scoped if it uses other scoped services
                    services.AddSingleton<UI>();
                })
                .Build();
            host.Start();

            IAffectedStatuesService AffectedStatuesService = host.Services.CreateScope().ServiceProvider.GetService<IAffectedStatuesService>();
            IAttributesService AttributesService = host.Services.CreateScope().ServiceProvider.GetService<IAttributesService>();
            IHeroService HeroService = host.Services.CreateScope().ServiceProvider.GetService<IHeroService>();
            IMonsterService MonsterService = host.Services.CreateScope().ServiceProvider.GetService<IMonsterService>();
            IResourcesService ResourcesService = host.Services.CreateScope().ServiceProvider.GetService<IResourcesService>();
            ITasksService TasksService = host.Services.CreateScope().ServiceProvider.GetService<ITasksService>();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var affectedStatuesService = services.GetRequiredService<IAffectedStatuesService>();
    var attributesService = services.GetRequiredService<IAttributesService>();
    var heroService = services.GetRequiredService<IHeroService>();
    var monsterService = services.GetRequiredService<IMonsterService>();
    var resourcesService = services.GetRequiredService<IResourcesService>();
    var tasksService = services.GetRequiredService<ITasksService>();

    var Ui = services.GetRequiredService<UI>();
    var jsonLoader = services.GetRequiredService<JsonDeserializer>();

    string data = "data.json";
    Console.Clear();
    jsonLoader.DeserializeJson(data);
    Ui.ShowMainMenu();
                
               
}

