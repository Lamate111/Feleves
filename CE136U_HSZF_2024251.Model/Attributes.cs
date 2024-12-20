using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{

        public class Attributes
        {
            [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int AttributeId { get; set; }

           [ForeignKey("HeroId")]
            public int HeroID { get; set; }
            [Required]
            public virtual Hero? Hero { get; set; }

            [Required]
            public int Health { get; set; } // Egészség érték
            [Required]
            public int Hunger { get; set; } // Éhség érték
            [Required]
            public int Thirst { get; set; } // Szomjúság érték
            [Required]
            public int Fatigue { get; set; } // Fáradtság érték


        }
}




