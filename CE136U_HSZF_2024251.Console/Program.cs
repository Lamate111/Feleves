using CE136U_HSZF_2024251.Application;
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
                    services.AddDbContext<TheWitchAppDataBaseContext>();
                    services.AddSingleton<IAbilitiesDataProvider, AbilitiesDataProvider>();
                    services.AddSingleton<IAttributesDataProvider, AttributesDataProvider>();
                    services.AddSingleton<ICharacterDataProvider, CharacterDataProvider>();
                    services.AddSingleton<IMonsterDataProvider, MonsterDataProvider>();
                    services.AddSingleton<IResourcesDataProvider, ResourcesDataProvider>();
                    services.AddSingleton<ITasksProvider, TasksDataProvider>();

                    services.AddSingleton<IAbilitiesService, AbilitiesService>();
                    services.AddSingleton<IAttributesService, AttributesService>();
                    services.AddSingleton<ICharacterService, CharacterService>();
                    services.AddSingleton<IMonsterService, MonsterService>();
                    services.AddSingleton<IResourcesService,ResourcesServices>();
                    services.AddSingleton<ITasksService,TaskService>();
                })
                .Build();
            host.Start();
        
           IAbilitiesService abilitiesService = host.Services.CreateScope().ServiceProvider.GetService<IAbilitiesService>();
           IAttributesService attributesService = host.Services.CreateScope().ServiceProvider.GetService<IAttributesService>();
           ICharacterService characterService = host.Services.CreateScope().ServiceProvider.GetService<ICharacterService>();
           IMonsterService monsterService = host.Services.CreateScope().ServiceProvider.GetService<IMonsterService>();
           IResourcesService resourcesService = host.Services.CreateScope().ServiceProvider.GetService<IResourcesService>();
           ITasksService tasksService = host.Services.CreateScope().ServiceProvider.GetService<ITasksService>();





        }
    }
}
