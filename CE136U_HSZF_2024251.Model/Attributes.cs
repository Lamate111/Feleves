using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{

        public class Attributes
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            public int HeroId { get; set; }

            [ForeignKey(nameof(HeroId))]
            public virtual Hero Hero { get; set; } = null;

            public int Health { get; set; } // Egészség érték

            public int Hunger { get; set; } // Éhség érték

            public int Thirst { get; set; } // Szomjúság érték

            public int Fatigue { get; set; } // Fáradtság érték
        }
    }




