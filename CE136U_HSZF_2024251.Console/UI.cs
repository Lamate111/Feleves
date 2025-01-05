using System;
using System.Collections.Generic;
using System.IO;
using CE136U_HSZF_2024251.Application;
using CE136U_HSZF_2024251.Model;
using Newtonsoft.Json;

namespace WitcherSurvival
{

    public class UI
    {
        
        private readonly IHeroService _heroService;
        private readonly ITasksService _taskService;
        private readonly IMonsterService _monsterService;
        private readonly IAffectedStatuesService _affectedSatuesService;
        private readonly IAttributesService _attributesService;
        private readonly IResourcesService _resourcesService;

        private List<Hero> team = new List<Hero> { };
        public UI(IHeroService heroService, ITasksService taskService, IMonsterService monsterService, IAffectedStatuesService affectedStatuesService, IAttributesService attributesService,
                IResourcesService resourcesService) // Use the interface
        {
            _heroService = heroService;
            _taskService = taskService;
            _monsterService = monsterService;
            _affectedSatuesService = affectedStatuesService;
            _attributesService = attributesService;
            _resourcesService = resourcesService;
        }

        private void ShowMainMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("==== Witcher Survival ====");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. List Tasks");
                Console.WriteLine("3. List Characters");
                Console.WriteLine("4. Make a character");
                Console.WriteLine("5. List Monsters");
                Console.WriteLine("6. Edit Team");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        StartGame();
                        break;
                    case "2":
                        ListTasks();
                        break;
                    case "3":
                        ListCharacters();
                        break;
                    case "4":
                        MakeCharacter();
                        break;
                    case "5":
                        ListMonsters();
                        break;
                    case "6":
                        AddCharacterToTeam();
                        break;
                    case "7":
                        return;
                       
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            } while (true);
        }

        //5
        private void ListMonsters()
        {
            Console.Clear();
            var monsters = _monsterService.GetMonsters();
            Console.WriteLine("==== Monster ====");
            foreach (var monster in monsters)
            {
                Console.WriteLine(monster);
            }
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();

        }

        //4
        private void MakeCharacter()
        {
            Console.Clear();
            Console.WriteLine("==== Character Maker / Editor ====");
            Console.Write("Make or Edit ? :");
            var answer = Console.ReadLine().ToLower();
            if (answer == "make")
            {
                Console.Clear();


                // 1. Ask for the character's name.
                Console.Write("Enter the character's name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Invalid name. Press Enter to go back.");
                    Console.ReadLine();
                    return;
                }

                // 2. Randomly generate health status.
                string[] healthStatuses = { "Healthy", "Injured", "Sick" };
                string healthStatus = healthStatuses[new Random().Next(healthStatuses.Length)];

                // 3. Randomly generate attributes (values between 50 and 100 for Health, and 0 to 100 for others).
                Random random = new Random();
                Attributes randomAttributes = new Attributes
                {
                    Health = random.Next(50, 101),
                    Hunger = random.Next(0, 101),
                    Thirst = random.Next(0, 101),
                    Fatigue = random.Next(0, 101)
                };

                // 4. Randomly generate resources (values between 0 and 50 for each resource).
                Resource randomResources = new Resource
                {
                    Food = random.Next(0, 51),
                    Water = random.Next(0, 51),
                    Weapons = random.Next(0, 51),
                    Alchemy_ingredients = random.Next(0, 51)
                };

                // 5. Ask for the character's abilities (comma-separated).
                Console.Write("Enter the character's abilities (e.g., combat, magic, healing): ");
                string abilitiesInput = Console.ReadLine();
                List<string> abilities = new List<string>(abilitiesInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                // 6. Create the hero with the generated data.
                Hero newHero = new Hero
                {
                    Name = name,
                    Health_status = healthStatus,
                    Attributes = randomAttributes,
                    Resources = randomResources,
                    Abilities = abilities
                };

                // 7. Save the new hero to the hero service.
                _heroService.Create(newHero);

                // 8. Display the created hero's details.
                Console.WriteLine("\nYou have successfully created a new character:");
                Console.WriteLine($"Name: {newHero.Name}");
                Console.WriteLine($"Health Status: {newHero.Health_status}");
                Console.WriteLine($"Attributes:");
                Console.WriteLine($"  - Health: {newHero.Attributes.Health}");
                Console.WriteLine($"  - Hunger: {newHero.Attributes.Hunger}");
                Console.WriteLine($"  - Thirst: {newHero.Attributes.Thirst}");
                Console.WriteLine($"  - Fatigue: {newHero.Attributes.Fatigue}");
                Console.WriteLine($"Resources:");
                Console.WriteLine($"  - Food: {newHero.Resources.Food}");
                Console.WriteLine($"  - Water: {newHero.Resources.Water}");
                Console.WriteLine($"  - Weapons: {newHero.Resources.Weapons}");
                Console.WriteLine($"  - Alchemy Ingredients: {newHero.Resources.Alchemy_ingredients}");
                Console.WriteLine($"Abilities: {string.Join(", ", newHero.Abilities)}");

                Console.WriteLine("\nPress Enter to return to the menu.");
                Console.ReadLine();
            }
            else if (answer == "edit")
            {
                Console.Clear();
                Console.Write("Who do you want to edit? (Full name please): ");
                ListCharacters();
                var answer_char = Console.ReadLine();

                // Find the character to edit
                var toBeEdited = _heroService.GetHeroes().FirstOrDefault(c => c.Name.Equals(answer_char, StringComparison.OrdinalIgnoreCase));

                if (toBeEdited == null)
                {
                    Console.WriteLine("Character not found. Press Enter to return to the menu.");
                    Console.ReadLine();
                    return;
                }

                Console.Clear();
                Console.WriteLine($"Editing character: {toBeEdited.Name}");

                // Edit health status
                Console.WriteLine($"Current Health Status: {toBeEdited.Health_status}");
                Console.Write("Enter new Health Status (Healthy, Injured, Sick): ");
                string newHealthStatus = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newHealthStatus))
                {
                    toBeEdited.Health_status = newHealthStatus;
                }

                // Edit attributes
                Console.WriteLine("Current Attributes:");
                Console.WriteLine($"  - Health: {toBeEdited.Attributes.Health}");
                Console.WriteLine($"  - Hunger: {toBeEdited.Attributes.Hunger}");
                Console.WriteLine($"  - Thirst: {toBeEdited.Attributes.Thirst}");
                Console.WriteLine($"  - Fatigue: {toBeEdited.Attributes.Fatigue}");

                Console.WriteLine("Enter new values (leave blank to keep current):");
                Console.Write("New Health (50-100): ");
                if (int.TryParse(Console.ReadLine(), out int newHealth))
                {
                    toBeEdited.Attributes.Health = newHealth;
                }
                Console.Write("New Hunger (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int newHunger))
                {
                    toBeEdited.Attributes.Hunger = newHunger;
                }
                Console.Write("New Thirst (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int newThirst))
                {
                    toBeEdited.Attributes.Thirst = newThirst;
                }
                Console.Write("New Fatigue (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int newFatigue))
                {
                    toBeEdited.Attributes.Fatigue = newFatigue;
                }

                // Edit resources
                Console.WriteLine("Current Resources:");
                Console.WriteLine($"  - Food: {toBeEdited.Resources.Food}");
                Console.WriteLine($"  - Water: {toBeEdited.Resources.Water}");
                Console.WriteLine($"  - Weapons: {toBeEdited.Resources.Weapons}");
                Console.WriteLine($"  - Alchemy Ingredients: {toBeEdited.Resources.Alchemy_ingredients}");

                Console.WriteLine("Enter new values (leave blank to keep current):");
                Console.Write("New Food (0-50): ");
                if (int.TryParse(Console.ReadLine(), out int newFood))
                {
                    toBeEdited.Resources.Food = newFood;
                }
                Console.Write("New Water (0-50): ");
                if (int.TryParse(Console.ReadLine(), out int newWater))
                {
                    toBeEdited.Resources.Water = newWater;
                }
                Console.Write("New Weapons (0-50): ");
                if (int.TryParse(Console.ReadLine(), out int newWeapons))
                {
                    toBeEdited.Resources.Weapons = newWeapons;
                }
                Console.Write("New Alchemy Ingredients (0-50): ");
                if (int.TryParse(Console.ReadLine(), out int newAlchemy))
                {
                    toBeEdited.Resources.Alchemy_ingredients = newAlchemy;
                }

                // Edit abilities
                Console.WriteLine("Current Abilities:");
                Console.WriteLine(string.Join(", ", toBeEdited.Abilities));
                Console.Write("Enter new abilities (comma-separated, leave blank to keep current): ");
                string newAbilitiesInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newAbilitiesInput))
                {
                    toBeEdited.Abilities = new List<string>(newAbilitiesInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }

                // Save the changes
                _heroService.Update(toBeEdited);

                Console.WriteLine("\nCharacter updated successfully!");
                Console.WriteLine("Press Enter to return to the menu.");
                Console.ReadLine();
            }
        }

        //1
        private void StartGame()
        {
            int Days = 0;
            int GameTime = 0;
            int[] CharacterTime = new int[team.Count];
            int[] Badpoints = new int[team.Count];
            bool[] Alive = new bool[team.Count];

            for (int i = 0; i < team.Count; i++)
            {
                CharacterTime[i] = 0;
                Badpoints[i] = 0;
                Alive[i] = true;
            }

            Console.Clear();
            Console.WriteLine("==== Welcome to the game ====");

            Console.WriteLine("Please set your difficulty: Easy // Normal // Hard");

            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "easy":
                    Console.WriteLine("You selected easy mode.");
                    Days = 1;
                    break;
                case "normal":
                    Console.WriteLine("You selected normal mode.");
                    Days = 3;
                    break;
                case "hard":
                    Console.WriteLine("You selected hard mode.");
                    Days = 5;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to normal mode.");
                    Days = 3;
                    break;
            }

            // Main game loop
            do
            {
                Console.Clear();
                Console.WriteLine($"Day {GameTime + 1} out of {Days}");
                Console.WriteLine("Choose an action for your team:");

                for (int i = 0; i < team.Count; i++)
                {
                    if (!Alive[i])
                    {
                        Console.WriteLine($"{team[i].Name} has died and can no longer take actions.");
                        continue;
                    }

                    if (CharacterTime[i] <= GameTime)
                    {
                        Console.WriteLine($"\n{team[i].Name} is ready for an action:");
                        Console.WriteLine("1. Perform a task");
                        Console.WriteLine("2. Rest");
                        Console.WriteLine("3. Consume resources");
                        Console.Write("Choose an option: ");
                        string actionChoice = Console.ReadLine();

                        //Check
                        if (team[i].Attributes.Thirst < 100 && 70 < team[i].Attributes.Thirst )
                        {
                            Console.WriteLine($"{team[i].Name} is a little thirsty");
                        }
                        if (team[i].Attributes.Thirst < 70 && team[i].Attributes.Thirst > 20)
                        {
                            Console.WriteLine($"{team[i].Name} is very thirsty ");
                        }
                        if (team[i].Attributes.Thirst < 20)
                        {
                            Console.WriteLine($"{team[i].Name} is dehyradted");
                            Badpoints[i]++;
                        }


                        switch (actionChoice)
                        {
                            case "1":
                                Console.WriteLine("Available tasks:");
                                ListTasks();
                                Console.Write("Choose a task by name: ");
                                string taskName = Console.ReadLine();

                                var task = _taskService.GetTasks().FirstOrDefault(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
                                if (task != null)
                                {
                                    Console.WriteLine($"{team[i].Name} is performing task: {task.Name}");
                                    CharacterTime[i] += task.Duration;
                                    team[i].Attributes.Fatigue += Int16.Parse(task.Affected_status.Fatigue.Remove('+'));
                                }
                                else
                                {
                                    Console.WriteLine("Task not found. No action taken.");
                                }
                                break;

                            case "2":
                                Console.WriteLine($"{team[i].Name} is resting.");
                                team[i].Attributes.Fatigue = Math.Max(0, team[i].Attributes.Fatigue - 20);
                                break;

                            case "3":
                                Console.WriteLine($"{team[i].Name} is consuming resources.");
                                if (team[i].Resources.Food > 0 && team[i].Resources.Water > 0)
                                {
                                    team[i].Resources.Food--;
                                    team[i].Resources.Water--;
                                    team[i].Attributes.Hunger = Math.Max(0, team[i].Attributes.Hunger - 30);
                                    team[i].Attributes.Thirst = Math.Max(0, team[i].Attributes.Thirst - 30);
                                }
                                else
                                {
                                    Console.WriteLine("Not enough resources available.");
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid action. No action taken.");
                                break;
                        }

                        // Check health and bad points
                        if (team[i].Attributes.Health <= 0 || Badpoints[i] == 3)
                        {
                            Console.WriteLine($"{team[i].Name} has died due to bad health or accumulated penalties.");
                            Alive[i] = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{team[i].Name} is occupied until time {CharacterTime[i]}.");
                    }
                }

                GameTime++;
            } while (GameTime < Days);

            Console.WriteLine("Game over! Here's the final report:");
            GenerateDetailedDailyReport();
            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
        }



        //2
        private void ListTasks()
        {
            Console.Clear();
            var tasks = _taskService.GetTasks();
            Console.WriteLine("==== Tasks ====");
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }


        private void ListCharacters()
        {
            Console.Clear();
            Console.WriteLine("(Filter By Ability name)");
            string k = Console.ReadLine();

            if (k == null)
            {
                Console.WriteLine("==== Characters ====");
                foreach (var character in _heroService.GetHeroes())
                {
                    Console.WriteLine(character);
                }

                ListTasks();
            }
            else
            {
                Console.WriteLine("==== Characters with skills ====");
                foreach (var character in _heroService.GetHeroes())
                {
                    if (character.Abilities.Contains(k))
                    {
                        Console.WriteLine(character);
                    }
                }
            }

            
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }

        

        private void AddCharacterToTeam()
        {
            Console.Clear();
            Console.WriteLine("==== Add Character to Team ====");
            ListCharacters();
            Console.Write("Enter the name of the character to add: ");
            string name = Console.ReadLine().ToLower();

            var character = _heroService.GetHeroes().FirstOrDefault(c => c.Name.ToLower().Equals(name, StringComparison.OrdinalIgnoreCase));
            if (character != null && !team.Contains(character))
            {
                team.Add(character);
                Console.WriteLine($"{character.Name} added to the team.");
            }
            else
            {
                Console.WriteLine("Character not found or already in the team.");
            }
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }

        private void ViewTeam()
        {
            Console.Clear();
            Console.WriteLine("==== Team ====");
            foreach (var member in team)
            {
                Console.WriteLine(member);
            }
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }

       

        private void GenerateDetailedDailyReport()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString("D2");
            string day = DateTime.Now.Day.ToString("D2");
            string folderPath = Path.Combine(year, month, day);

            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, "DailyReport.xml");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("<DailyReport>");
                foreach (var member in team)
                {
                    writer.WriteLine($"  <Character name=\"{member.Name}\">");
                    writer.WriteLine($"    <Health>{member.Attributes.Health}</Health>");
                    writer.WriteLine($"    <Hunger>{member.Attributes.Hunger}</Hunger>");
                    writer.WriteLine($"    <Thirst>{member.Attributes.Thirst}</Thirst>");
                    writer.WriteLine($"    <Tasks>");
                    foreach (var task in member.Tasks)
                    {
                        writer.WriteLine($"      <Task name=\"{task.Name}\" />");
                    }
                    writer.WriteLine($"    </Tasks>");
                    writer.WriteLine("  </Character>");
                }
                writer.WriteLine("</DailyReport>");
            }

            Console.WriteLine($"Daily report saved to {filePath}");
        }

    }
}
