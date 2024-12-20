using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{
    public class Hero
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HeroId { get; set; }
        [Required]
        public string? Name { get; set; }


        public string? Health_status { get; set; } // Egészségi állapot (pl. healthy, injured)

        [Required]
        public virtual Attributes? Attributes { get; set; }

        public List<string> Abilities { get; set; } // Képességek listája

        [Required]
        public virtual Resource? Resources { get; set; }

        public virtual ICollection<Tasks>? Tasks { get; set; } = [];

        public Hero()
        {
            Abilities = [];
            Tasks = [];
        }
    }


}
