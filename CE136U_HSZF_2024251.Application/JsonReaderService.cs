using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using CE136U_HSZF_2024251.Model;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace CE136U_HSZF_2024251.Application
{
    //public class JsonDeserializer
    //{
    //    private readonly IHeroService _heroService;
    //    private readonly ITasksService _taskService;
    //    private readonly IMonsterService _monsterService;

    //    public JsonDeserializer(IHeroService heroService, ITasksService taskService, IMonsterService monsterService)
    //    {
    //        _heroService = heroService;
    //        _taskService = taskService;
    //        _monsterService = monsterService;
    //    }

    //    public void DeserializeJson(string filePath)
    //    {
    //        if (!File.Exists(filePath))
    //        {
    //            Console.WriteLine("File not found: " + filePath);
    //            return;
    //        }

    //        string jsonContent = File.ReadAllText(filePath);

    //        // Deserialize JSON into a dictionary
    //        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);

    //        // Process "heroes"
    //        if (jsonData.ContainsKey("heroes"))
    //        {
    //            var heroesJson = JsonConvert.DeserializeObject<List<Hero>>(jsonData["heroes"].ToString());
    //            foreach (var hero in heroesJson)
    //            {

    //                    var newHero = new Hero
    //                    {
    //                        Name = hero.Name,
    //                        HealthStatus = hero.HealthStatus,
    //                        Attributes = new Attributes
    //                        {
    //                            Health = hero.Attributes.Health,
    //                            Hunger = hero.Attributes.Hunger,
    //                            Thirst = hero.Attributes.Thirst,
    //                            Fatigue = hero.Attributes.Fatigue
    //                        },
    //                        Abilities = hero.Abilities,
    //                        Resources = new Resource
    //                        {
    //                            Food = hero.Resources.Food,
    //                            Water = hero.Resources.Water,
    //                            Weapons = hero.Resources.Weapons,
    //                            AlchemyIngredients = hero.Resources.AlchemyIngredients
    //                        }
    //                    };
    //                _heroService.Create(newHero);
    //                }

    //            }

    //        // Process "tasks"
    //        if (jsonData.ContainsKey("tasks"))
    //        {
    //            var tasksJson = JsonConvert.DeserializeObject<List<Tasks>>(jsonData["tasks"].ToString());
    //            foreach (var task in tasksJson)
    //            {

    //                    var newTask = new Tasks
    //                    {
    //                        Name = task.Name,
    //                        Duration = task.Duration,
    //                        RequiredResources = new Resource
    //                        {
    //                            Food = task.RequiredResources.Food,
    //                            Water = task.RequiredResources.Water,
    //                            Weapons = task.RequiredResources.Weapons,
    //                            AlchemyIngredients = task.RequiredResources.AlchemyIngredients
    //                        },
    //                        AffectedStatus = new AffectedStatues
    //                        {
    //                            Health = task.AffectedStatus.Health,
    //                            Hunger = task.AffectedStatus.Hunger,
    //                            Thirst = task.AffectedStatus.Thirst,
    //                            Fatigue = task.AffectedStatus.Fatigue
    //                        },
    //                        Reward = task.Reward != null
    //                            ? new Resource
    //                            {
    //                                Food = task.Reward.Food,
    //                                Water = task.Reward.Water,
    //                                Weapons = task.Reward.Weapons,
    //                                AlchemyIngredients = task.Reward.AlchemyIngredients
    //                            }
    //                            : null
    //                    };
    //                    _taskService.Create(newTask);
    //                }
    //            }


    //        // Process "monsters"
    //        if (jsonData.ContainsKey("monsters"))
    //        {
    //            var monstersJson = JsonConvert.DeserializeObject<List<Monster>>(jsonData["monsters"].ToString());
    //            foreach (var monster in monstersJson)
    //            {

    //                    var newMonster = new Monster
    //                    {
    //                        Name = monster.Name,
    //                        Difficulty = monster.Difficulty,
    //                        RequiredAbility = monster.RequiredAbility,
    //                        Loot = new Resource
    //                        {
    //                            Food = monster.Loot.Food,
    //                            Water = monster.Loot.Water,
    //                            Weapons = monster.Loot.Weapons,
    //                            AlchemyIngredients = monster.Loot.AlchemyIngredients
    //                        }
    //                    };
    //                    _monsterService.Create(newMonster);
    //                }
    //            }
    //        }

    //    }
    namespace CE136U_HSZF_2024251.Application
    {
        public class GameData
        {
            public List<Hero> Heroes { get; set; }
            public List<Tasks> Tasks { get; set; }
            public List<Monster> Monsters { get; set; }
        }

        public class JsonDeserializer
        {
            private readonly IHeroService _heroService;
            private readonly ITasksService _taskService;
            private readonly IMonsterService _monsterService;

            public JsonDeserializer(IHeroService heroService, ITasksService taskService, IMonsterService monsterService)
            {
                _heroService = heroService;
                _taskService = taskService;
                _monsterService = monsterService;
            }

            public void DeserializeJson(string filePath)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found: " + filePath);
                    return;
                }

                string jsonContent = File.ReadAllText(filePath);
                var gameData = JsonConvert.DeserializeObject<GameData>(jsonContent);

                if (gameData != null)
                {
                    foreach (var hero in gameData.Heroes)
                    {
                        _heroService.Create(hero); // Assuming Create method exists in IHeroService
                    }

                    foreach (var task in gameData.Tasks)
                    {
                        _taskService.Create(task); // Assuming Create method exists in ITasksService
                    }

                    foreach (var monster in gameData.Monsters)
                    {
                        _monsterService.Create(monster); // Assuming Create method exists in IMonsterService
                    }
                }
                else
                {
                    Console.WriteLine("Deserialization failed.");
                }
            }
        }
    }
}




