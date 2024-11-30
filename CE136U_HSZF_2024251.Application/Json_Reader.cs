using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using CE136U_HSZF_2024251.Model;

namespace CE136U_HSZF_2024251.Application
{
    public class JsonDeserializer
    {

        private readonly IAbilitiesService _abilitiesService;
        private readonly IAttributesService _attributesService;
        private readonly ICharacterService _characterService;
        private readonly IMonsterService _monsterService;
        private readonly IResourcesService _resourcesService;
        private readonly ITasksService _tasksService;

        public JsonDeserializer(IAbilitiesService abilitiesService, IAttributesService attributesService, ICharacterService characterService, IMonsterService monsterService, IResourcesService resourcesService, ITasksService tasksService)
        {
            _abilitiesService = abilitiesService;
            _attributesService = attributesService;
            _characterService = characterService;
            _monsterService = monsterService;
            _resourcesService = resourcesService;
            _tasksService = tasksService;
        }
        public void reader(string route) 
        {
            string json = File.ReadAllText(route);
            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            // Deserialize each part separately
            List<Character> heroes = JsonSerializer.Deserialize<List<Character>>(root.GetProperty("heroes").GetRawText());
            foreach (Character character in heroes) 
            {
                _characterService.Create(character);
            }
            List<Tasks> tasks = JsonSerializer.Deserialize<List<Tasks>>(root.GetProperty("tasks").GetRawText());
            foreach (Tasks task in tasks)
            {
                _tasksService.Create(task);
            }
            List<Monsters> monsters = JsonSerializer.Deserialize<List<Monsters>>(root.GetProperty("monsters").GetRawText());
            foreach (Monsters monster in monsters)
            {
                _monsterService.Create(monster);
            }
        }
    }
}
