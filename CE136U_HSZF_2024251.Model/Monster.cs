using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CE136U_HSZF_2024251.Model
{
        public class Monster
        {
            
            [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int MonsterId { get; set; }

            [Required]
            public string? Name { get; set; } // Szörny neve

 
            public int Difficulty { get; set; } // Nehézségi szint


            public string? Required_ability { get; set; } // Szükséges képesség (pl. "combat")

            public virtual Resource? Loot { get; set; } // Zsákmány (loot)

        public override string? ToString()
        {
            return $"{Name}" +
                $"\n Difficulty: {Difficulty}" +
                $"\n Weak to: {Required_ability}\n" +
                $"LOOT: {Loot}";
        }
    }
    }
