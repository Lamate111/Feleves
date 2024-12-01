using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{
    public class Hero
    {
        [Key]
        public int HeroId { get; set; }

        public string? Name { get; set; }


        public string? HealthStatus { get; set; } // Egészségi állapot (pl. healthy, injured)

        [Required]
        public virtual Attributes Attributes { get; set; }

        public List<string> Abilities { get; set; } // Képességek listája

        [Required]
        public virtual Resource Resources { get; set; }

        [Required]
        public virtual ICollection<Tasks> Tasks { get; } = new List<Tasks>();

        public Hero()
        {
            Abilities = new List<string>();
        }
    }


}
