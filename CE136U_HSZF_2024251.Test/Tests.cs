using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using CE136U_HSZF_2024251.Application;
using CE136U_HSZF_2024251.Model;
using NUnit.Framework.Legacy;

namespace WitcherSurvival.Tests
{
    [TestFixture]
    public class UITests
    {
        private Mock<IHeroService> _heroServiceMock;
        private Mock<ITasksService> _taskServiceMock;
        private Mock<IMonsterService> _monsterServiceMock;
        private UI _ui;

        [SetUp]
        public void Setup()
        {
            _heroServiceMock = new Mock<IHeroService>();
            _taskServiceMock = new Mock<ITasksService>();
            _monsterServiceMock = new Mock<IMonsterService>();

            _ui = new UI(
                _heroServiceMock.Object,
                _taskServiceMock.Object,
                _monsterServiceMock.Object,
                null, null, null
            );
        }

        [Test]
        public void ListTasks_ShouldDisplayAllTasks()
        {
            // Arrange
            var tasks = new List<Tasks>
            {
                new Tasks { Name = "Task 1" },
                new Tasks { Name = "Task 2" }
            };
            _taskServiceMock.Setup(t => t.GetTasks()).Returns(tasks);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListTasks();

            // Assert
            StringAssert.Contains("==== Tasks ====", output.ToString());
            StringAssert.Contains("Task 1", output.ToString());
            StringAssert.Contains("Task 2", output.ToString());
        }

        [Test]
        public void ListTasks_ShouldHandleNoTasks()
        {
            // Arrange
            var tasks = new List<Tasks>();
            _taskServiceMock.Setup(t => t.GetTasks()).Returns(tasks);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListTasks();

            // Assert
            StringAssert.Contains("==== Tasks ====", output.ToString());
            StringAssert.DoesNotContain("Task", output.ToString());
        }

        [Test]
        public void ListMonsters_ShouldDisplayAllMonsters()
        {
            // Arrange
            var monsters = new List<Monster>
            {
                new Monster { Name = "Monster 1" },
                new Monster { Name = "Monster 2" }
            };
            _monsterServiceMock.Setup(m => m.GetMonsters()).Returns(monsters);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListMonsters();

            // Assert
            StringAssert.Contains("==== Monster ====", output.ToString());
            StringAssert.Contains("Monster 1", output.ToString());
            StringAssert.Contains("Monster 2", output.ToString());
        }

        [Test]
        public void ListMonsters_ShouldHandleNoMonsters()
        {
            // Arrange
            var monsters = new List<Monster>();
            _monsterServiceMock.Setup(m => m.GetMonsters()).Returns(monsters);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListMonsters();

            // Assert
            StringAssert.Contains("==== Monster ====", output.ToString());
            StringAssert.DoesNotContain("Monster", output.ToString());
        }

        [Test]
        public void ListCharacters_ShouldDisplayAllCharacters_WhenNoFilter()
        {
            // Arrange
            var characters = new List<Hero>
            {
                new Hero { Name = "Hero 1" },
                new Hero { Name = "Hero 2" }
            };

            _heroServiceMock.Setup(h => h.GetHeroes()).Returns(characters);

            var inputSequence = new List<string> { "", "Enter" }; // Simulate pressing Enter for no filter
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputSequence)));

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListCharacters();

            // Assert
            StringAssert.Contains("==== Characters ====", output.ToString());
            StringAssert.Contains("Hero 1", output.ToString());
            StringAssert.Contains("Hero 2", output.ToString());
        }

        [Test]
        public void ListCharacters_ShouldFilterByAbility_WhenFilterProvided()
        {
            // Arrange
            var characters = new List<Hero>
           {
               new Hero { Name = "Hero 1", Abilities = new List<string> { "Archery" } },
               new Hero { Name = "Hero 2", Abilities = new List<string> { "Swordsmanship" } }
           };

            _heroServiceMock.Setup(h => h.GetHeroes()).Returns(characters);

            var inputSequence = new List<string> { "archery", "Enter" }; // Simulate filtering by ability
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputSequence)));

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListCharacters();

            // Assert
            StringAssert.Contains("==== Characters with skills ====", output.ToString());
            StringAssert.Contains("Hero 1", output.ToString());
            StringAssert.DoesNotContain("Hero 2", output.ToString());
        }

        [Test]
        public void ListCharacters_ShouldHandleNoMatchingAbility()
        {
            // Arrange
            var characters = new List<Hero>
           {
               new Hero { Name = "Hero 1", Abilities = new List<string> { "Archery" } },
               new Hero { Name = "Hero 2", Abilities = new List<string> { "Swordsmanship" } }
           };

            _heroServiceMock.Setup(h => h.GetHeroes()).Returns(characters);

            var inputSequence = new List<string> { "Magic", "Enter" }; // Simulate filtering by non-existing ability
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputSequence)));

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListCharacters();

            // Assert
            StringAssert.Contains("==== Characters with skills ====", output.ToString());
            StringAssert.DoesNotContain("Hero 1", output.ToString());
            StringAssert.DoesNotContain("Hero 2", output.ToString());
        }

        [Test]
        public void ListCharacters_ShouldHandleEmptyInput()
        {
            // Arrange: No heroes available.
            var characters = new List<Hero>();
            _heroServiceMock.Setup(h => h.GetHeroes()).Returns(characters);

            var inputSequence = new List<string> { "", "Enter" }; // Simulate pressing Enter for no filter.
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputSequence)));

            var output = new StringWriter();
            Console.SetOut(output);

            // Act: Call the method.
            _ui.ListCharacters();

            // Assert: Ensure that no characters are listed.
            StringAssert.Contains("==== Characters ====", output.ToString());
            StringAssert.DoesNotContain("Hero", output.ToString());
        }
        [Test]
        public void ListTasks_ShouldDisplayTaskDetails()
        {
            // Arrange
            var tasks = new List<Tasks>
    {
        new Tasks { Name = "Task 1", Duration = 2 },
        new Tasks { Name = "Task 2", Duration = 3 }
    };
            _taskServiceMock.Setup(t => t.GetTasks()).Returns(tasks);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListTasks();

            // Assert
            StringAssert.Contains("==== Tasks ====", output.ToString());
            StringAssert.Contains("Task 1", output.ToString());
            StringAssert.Contains("Task 2", output.ToString());
        }

        [Test]
        public void ListMonsters_ShouldDisplayMonsterDetails()
        {
            // Arrange
            var monsters = new List<Monster>
    {
        new Monster { Name = "Monster 1", Difficulty = 3 },
        new Monster { Name = "Monster 2", Difficulty = 5 }
    };
            _monsterServiceMock.Setup(m => m.GetMonsters()).Returns(monsters);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListMonsters();

            // Assert
            StringAssert.Contains("==== Monster ====", output.ToString());
            StringAssert.Contains("Monster 1", output.ToString());
            StringAssert.Contains("Monster 2", output.ToString());
        }

        [Test]
        public void ListCharacters_ShouldDisplayCharacterAbilities_WhenFilterProvided()
        {
            // Arrange
            var characters = new List<Hero>
   {
       new Hero { Name = "Hero 1", Abilities = new List<string> { "Archery" } },
       new Hero { Name = "Hero 2", Abilities = new List<string> { "Swordsmanship" } }
   };

            _heroServiceMock.Setup(h => h.GetHeroes()).Returns(characters);

            var inputSequence = new List<string> { "archery", "Enter" }; // Simulate filtering by ability
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputSequence)));

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            _ui.ListCharacters();

            // Assert
            StringAssert.Contains("==== Characters with skills ====", output.ToString());
            StringAssert.Contains("Hero 1", output.ToString());
            StringAssert.DoesNotContain("Hero 2", output.ToString());
        }

        [Test]
        public void ListCharacters_ShouldDisplayNoCharacters_WhenNoHeroesAvailable()
        {
            // Arrange: No heroes available.
            var characters = new List<Hero>();
            _heroServiceMock.Setup(h => h.GetHeroes()).Returns(characters);

            var inputSequence = new List<string> { "", "Enter" }; // Simulate pressing Enter for no filter.
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputSequence)));

            var output = new StringWriter();
            Console.SetOut(output);

            // Act: Call the method.
            _ui.ListCharacters();

            // Assert: Ensure that no characters are listed.
            StringAssert.Contains("==== Characters ====", output.ToString());
            StringAssert.DoesNotContain("Hero", output.ToString());
        }

    }
}
