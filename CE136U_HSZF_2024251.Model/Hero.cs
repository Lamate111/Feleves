using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{
    public class Hero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HeroId { get; set; }

        public string Name { get; set; }


        public string? HealthStatus { get { return "healthy"; } set { if (value != "healthy") ; } } // Egészségi állapot (pl. healthy, injured)

        public virtual Attributes Attributes { get; set; }

        public List<string> Abilities { get; set; } // Képességek listája


        public virtual Resource Resources { get; set; }

        public virtual ICollection<Tasks>? Tasks { get; set; } = new List<Tasks>();

        public Hero()
        {
            Abilities = new List<string>();
            Tasks = new List<Tasks>();
        }
    }


}
