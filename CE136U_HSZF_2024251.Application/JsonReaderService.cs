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
using Microsoft.Data.SqlClient;

namespace CE136U_HSZF_2024251.Application
{
    public class GameData
    {
        public List<Hero>? Heroes { get; set; }
        public List<Tasks>? Tasks { get; set; }
        public List<Monster>? Monsters { get; set; }
    }

    public class JsonDeserializer
    {
        private readonly IHeroService _heroService;
        private readonly ITasksService _taskService;
        private readonly IMonsterService _monsterService;
        private readonly IAffectedStatuesService _affectedSatuesService;
        private readonly IAttributesService _attributesService;
        private readonly IResourcesService _resourcesService;


        public JsonDeserializer(IHeroService heroService, ITasksService taskService, IMonsterService monsterService, IAffectedStatuesService affectedStatuesService, IAttributesService attributesService,
                IResourcesService resourcesService) // Use the interface
        {
            _heroService = heroService;
            _taskService = taskService;
            _monsterService = monsterService;
            _affectedSatuesService = affectedStatuesService;
            _attributesService = attributesService;
            _resourcesService = resourcesService;
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
                    Hero temp = new Hero()
                    {
                        Name = hero.Name,
                        Health_status = hero.Health_status,
                        Abilities = hero.Abilities,
                        Attributes = new Attributes()
                        {
                            Health = hero.Attributes.Health,
                            Fatigue = hero.Attributes.Fatigue,
                            Hunger = hero.Attributes.Hunger,
                            Thirst = hero.Attributes.Thirst
                        },
                        Resources = new Resource()
                        {
                            Food = hero.Resources.Food,
                            Alchemy_ingredients = hero.Resources.Alchemy_ingredients,
                            Water = hero.Resources.Water,
                            Weapons = hero.Resources.Weapons
                        }

                    };

                    _heroService.Create(temp);
                    // Assuming Create method exists in IHeroService
                }

                foreach (var task in gameData.Tasks ?? new List<Tasks>())
                {
                    Tasks temp = new Tasks()
                    {
                        Name = task.Name,
                        Duration = task.Duration,

                        Required_resources = task.Required_resources != null ? new Resource()
                        {
                            Food = task.Required_resources.Food,
                            Water = task.Required_resources.Water,
                            Alchemy_ingredients = task.Required_resources.Alchemy_ingredients,
                            Weapons = task.Required_resources.Weapons
                        } : null,

                        Affected_status = task.Affected_status != null ? new AffectedStatues()
                        {
                            Fatigue = task.Affected_status.Fatigue,
                            Health = task.Affected_status.Health,
                            Hunger = task.Affected_status.Hunger,
                            Thirst = task.Affected_status.Thirst
                        } : null,

                        Reward = task.Reward != null ? new Resource()
                        {
                            Food = task.Reward.Food,
                            Water = task.Reward.Water,
                            Alchemy_ingredients = task.Reward.Alchemy_ingredients,
                            Weapons = task.Reward.Weapons
                        } : null,
                    };

                    _taskService.Create(temp);
                }



                foreach (var monster in gameData.Monsters ?? [])
                {
                    Monster temp = new Monster()
                    {
                        Name = monster.Name,
                        Difficulty = monster.Difficulty,
                        Required_ability = monster.Required_ability,

                        Loot = monster.Loot != null ? new Resource()
                        {
                            Alchemy_ingredients = monster.Loot.Alchemy_ingredients,
                            Food = monster.Loot.Food,
                            Water = monster.Loot.Water,
                            Weapons = monster.Loot.Weapons
                        } : null
                    };

                    _monsterService.Create(temp);
                }

            }

        }

        }

    }
    






