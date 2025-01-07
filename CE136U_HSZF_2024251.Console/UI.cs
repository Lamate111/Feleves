using System;
using System.Collections.Generic;
using System.IO;
using CE136U_HSZF_2024251.Application;
using CE136U_HSZF_2024251.Model;
using Microsoft.IdentityModel.Tokens;
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
        private Resource inventory = new Resource();
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

        public void ShowMainMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("==== Witcher Survival ====");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. List Tasks");
                Console.WriteLine("3. List Characters");
                Console.WriteLine("4. List Monsters");
                Console.WriteLine("5. Make or Edit character");
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
                        ListMonsters();
                        break;
                    case "5":
                        MakeorEditCharacter();
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

        private void StartGame()
        {
            int DaysToWin = 0;
            int CurrentDays = 0;
            int GameTime = 0;

            var standard = _heroService.GetHeroes();
            if (team.Count == 0)
            {
                foreach (var hero in standard)
                {
                    team.Add(hero);
                }
            }

            int[] CharacterTime = new int[team.Count];
            int[] Badpoints = new int[team.Count];
            bool[] Alive = new bool[team.Count];
            bool IsPressed = false;
            

            for (int i = 0; i < team.Count; i++)
            {
                CharacterTime[i] = 0;
                Badpoints[i] = 0;
                Alive[i] = true;
            }

            Console.Clear();
            FillInventory();
            Console.WriteLine("==== Welcome to the game ====");

            Console.WriteLine("Please set your difficulty: Easy // Normal // Hard");

            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "easy":
                    Console.WriteLine("You selected easy mode.");
                    DaysToWin = 2;
                    break;
                case "normal":
                    Console.WriteLine("You selected normal mode.");
                    DaysToWin = 3;
                    break;
                case "hard":
                    Console.WriteLine("You selected hard mode.");
                    DaysToWin = 5;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to normal mode.");
                    DaysToWin = 3;
                    break;
            }

            // Main game loop
            do
            {
                Console.Clear();
                Console.WriteLine($"Day {CurrentDays + 1} out of {DaysToWin}");
                Console.WriteLine($"Current Hour {GameTime}");
                Console.WriteLine("Choose an action for your team:");

                // Display team stats and inventory at the beginning of each loop
                Console.WriteLine("\n====== TEAM STATUS ======");
                foreach (var member in team)
                {
                    if (member.Attributes.Health > 0)
                    {
                        Console.WriteLine($"{member.Name}: " +
                            $"\n Health = {member.Attributes.Health}," +
                            $"\n Hunger = {member.Attributes.Hunger}," +
                            $"\n Thirst = {member.Attributes.Thirst}, " +
                            $"\n Fatigue = {member.Attributes.Fatigue}, " +
                            $"\n Status = {member.Health_status}");
                    }
                    else
                    {
                        Console.WriteLine($"{member.Name}: Status = Dead or Unavailable");
                    }
                }

                Console.WriteLine("\n====== INVENTORY ======");
                Console.WriteLine(inventory.ToString());

                for (int i = 0; i < team.Count; i++)
                {
                    if (team[i].Attributes.Health <= 0 || Badpoints[i] == 3)
                    {
                        Console.WriteLine($"{team[i].Name} has died or left due to bad health or accumulated penalties.");
                        Alive[i] = false;
                    }

                    if (Alive[i])
                    {
                        int health = team[i].Attributes.Health;
                        int thirst = team[i].Attributes.Thirst;
                        int hunger = team[i].Attributes.Hunger;

                        // Health status check using switch expression
                        team[i].Health_status = health switch
                        {
                            <= 50 => "Sick",
                            <= 70 => "Injured",
                            _ => "Healthy"
                        };

                        Console.WriteLine($"\n{team[i].Name} is {team[i].Health_status}");

                        // Thirst status check using switch expression
                        switch (thirst)
                        {
                            case < 20:
                                Console.WriteLine($"{team[i].Name} is little thirsty");
                                break;
                            case < 70:
                                Console.WriteLine($"{team[i].Name} is thirsty");
                                break;
                            case > 70 and < 100:
                                Console.WriteLine($"{team[i].Name} is dehydrated");
                                Badpoints[i]++;
                                break;
                        }

                        // Hunger status check using switch expression
                        switch (hunger)
                        {
                            case < 20:
                                Console.WriteLine($"{team[i].Name} is little hungry");
                                break;
                            case < 70:
                                Console.WriteLine($"{team[i].Name} is hungry");
                                break;
                            case > 70 and < 100:
                                Console.WriteLine($"{team[i].Name} is Malnourished");
                                Badpoints[i]++;
                                break;
                        }

                    }
                    else
                    {
                        Console.WriteLine($"{team[i].Name} has died and can no longer take actions.");
                        continue;
                    }

                    // Display current stats and inventory before every decision
                    Console.WriteLine("\n====== CURRENT CHARACTER STATUS ======");
                    Console.WriteLine($"{team[i].Name}: Health = {team[i].Attributes.Health}, Hunger = {team[i].Attributes.Hunger}, Thirst = {team[i].Attributes.Thirst}, Fatigue = {team[i].Attributes.Fatigue}, Status = {team[i].Health_status}");
                    Console.WriteLine("\n====== CURRENT INVENTORY ======");
                    Console.WriteLine(inventory.ToString());

                    if (CharacterTime[i] <= GameTime)
                    {
                        Console.WriteLine($"\n{team[i].Name} is ready for an action:");
                        Console.WriteLine("1. Perform a task");
                        Console.WriteLine("2. Rest");
                        Console.WriteLine("3. Consume resources");
                        Console.Write("Choose an option: ");
                        string actionChoice = Console.ReadLine();

                        switch (actionChoice)
                        {
                            case "1":
                                Console.WriteLine("======= Available tasks =======");
                                ListTasks();
                                Console.Write("Choose a task by name: ");
                                string taskName = Console.ReadLine();

                                var task = _taskService.GetTasks().FirstOrDefault(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
                                if (task != null)
                                {
                                    Console.WriteLine($"{team[i].Name} is performing task: {task.Name}");

                                    // Check if the required resources are available
                                    bool hasEnoughResources = true;
                                    Type resourceType = typeof(Resource);
                                    foreach (var property in resourceType.GetProperties())
                                    {
                                        string requiredResourceValue = property.GetValue(task.Required_resources)?.ToString() ?? "0";
                                        if (int.TryParse(requiredResourceValue, out int requiredAmount))
                                        {
                                            int currentInventoryValue = (int)property.GetValue(inventory);
                                            if (currentInventoryValue < requiredAmount)
                                            {
                                                Console.WriteLine($"Not enough {property.Name} in inventory to perform the task!");
                                                hasEnoughResources = false;
                                                break;
                                            }
                                        }
                                    }

                                    if (!hasEnoughResources)
                                    {
                                        Console.WriteLine("Task cannot be performed due to insufficient resources.");
                                        break;
                                    }

                                    // Deduct the required resources from inventory
                                    foreach (var property in resourceType.GetProperties())
                                    {
                                        string requiredResourceValue = property.GetValue(task.Required_resources)?.ToString() ?? "0";
                                        if (int.TryParse(requiredResourceValue, out int requiredAmount))
                                        {
                                            int currentInventoryValue = (int)property.GetValue(inventory);
                                            property.SetValue(inventory, currentInventoryValue - requiredAmount);
                                        }
                                    }

                                    // Task consequences
                                    CharacterTime[i] += task.Duration;

                                    // Adjust hero attributes
                                    team[i].Attributes.Fatigue += int.Parse(task.Affected_status.Fatigue?.Replace("+", "") ?? "0");
                                    team[i].Attributes.Hunger += int.TryParse(task.Affected_status.Hunger, out int hungerValue) ? hungerValue : 0;
                                    team[i].Attributes.Thirst += int.TryParse(task.Affected_status.Thirst, out int thirstValue) ? thirstValue : 0;
                                    team[i].Attributes.Health += task.Affected_status.Health ?? 0;

                                    // Add the task rewards to inventory
                                    foreach (var property in resourceType.GetProperties())
                                    {
                                        string rewardResourceValue = property.GetValue(task.Reward)?.ToString() ?? "0";
                                        if (int.TryParse(rewardResourceValue, out int rewardAmount))
                                        {
                                            int currentInventoryValue = (int)property.GetValue(inventory);
                                            property.SetValue(inventory, currentInventoryValue + rewardAmount);
                                        }
                                    }

                                    Console.WriteLine("Task completed! Rewards and penalties applied to inventory and attributes.");
                                    Console.WriteLine($"Current inventory = {inventory.ToString()}");
                                }
                                else
                                {
                                    Console.WriteLine("Task not found. No action taken.");
                                }
                                break;

                            case "2":
                                Console.WriteLine($"{team[i].Name} is resting.");
                                team[i].Attributes.Fatigue = Math.Max(0, team[i].Attributes.Fatigue - 20);
                                CharacterTime[i] += 1;
                                break;

                            case "3":
                                Console.WriteLine($"{team[i].Name} is consuming resources.");
                                if (team[i].Resources.Food > 0 && team[i].Resources.Water > 0)
                                {
                                    team[i].Resources.Food--;
                                    team[i].Resources.Water--;
                                    team[i].Attributes.Hunger = Math.Max(0, team[i].Attributes.Hunger - 30);
                                    team[i].Attributes.Thirst = Math.Max(0, team[i].Attributes.Thirst - 30);
                                    CharacterTime[i] += 1;
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
                    }
                    else
                    {
                        Console.WriteLine($"{team[i].Name} is occupied until time {CharacterTime[i]}.");
                    }
                }

                GameTime++;
                GenerateDetailedDailyReport();

                if (GameTime / 24 == 1)
                {
                    GameTime = 0;
                    CurrentDays++;
                    Console.WriteLine("Are you sure you want to continue? (y/n)");
                    var answer = Console.ReadLine();
                    if (answer != "y") IsPressed = true;
                }
            } while (CurrentDays < DaysToWin && !IsPressed);


            Console.WriteLine("Game over! Here's the final report:");
            GenerateDetailedDailyReport();
            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
        }


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
        private void MakeorEditCharacter()
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
                QucikListCharacters();
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

        private void QucikListCharacters()
        {
            Console.Clear();
            Console.WriteLine("==== Characters ====");
            foreach (var character in _heroService.GetHeroes())
            {
                Console.WriteLine(character);
            }

        }
    private void ListCharacters()
        {
            Console.Clear();

            Console.WriteLine("(Filter By Ability name) or just press Enter");

            string k = Console.ReadLine().ToLower();

            if (k.IsNullOrEmpty())
            {
                Console.WriteLine("==== Characters ====");
                foreach (var character in _heroService.GetHeroes())
                {
                    Console.WriteLine(character);
                }

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
    QucikListCharacters();
    bool EndPressed = false;
    Console.WriteLine("Press ESC to exit");

    do
    {
        QucikListCharacters();

        if (team.Count != 0)
        {
            Console.WriteLine("==== Currently in the team ====");
            foreach (var hero in team)
            {
                Console.Write($"{hero.Name}, ");
            }
            Console.WriteLine("\n");
        }

        Console.Write("Enter the name of the character to add or remove: ");
        string name = Console.ReadLine().ToLower();

        var character = _heroService.GetHeroes().FirstOrDefault(c => c.Name.ToLower().Equals(name, StringComparison.OrdinalIgnoreCase));

        if (character != null)
        {
            if (team.Contains(character))
            {
                // Remove the character if they are already in the team
                team.Remove(character);
                Console.Clear();
                Console.WriteLine($"{character.Name} removed from the team.");
            }
            else if (team.Count < 4)
            {
                // Add the character if the team is not full
                team.Add(character);
                Console.WriteLine($"{character.Name} added to the team.");
            }
            else
            {
                // Team is full and character is not in the team
                Console.Clear();
                Console.WriteLine("The team is full! You must remove a character before adding another.");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Character not found.");
        }

        Console.WriteLine("\nPress ESC to exit or any other key to continue.");
        if (Console.ReadKey().Key == ConsoleKey.Escape) EndPressed = true;

    } while (!EndPressed);

    Console.WriteLine("\nPress Enter to return to the menu.");
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

        private void FillInventory()
        {
            foreach (var hero in team)
            {
                inventory.Alchemy_ingredients += hero.Resources.Alchemy_ingredients;
                inventory.Food += hero.Resources.Food;
                inventory.Water += hero.Resources.Water;
                inventory.Weapons += hero.Resources.Weapons;
            }

        }

    }
}
