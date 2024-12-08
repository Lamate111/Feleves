﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{

        public class Attributes
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int AttributeId { get; set; }

           [ForeignKey("HeroId")]
            public int HeroID_attr { get; set; }
            public virtual Hero Hero { get; set; } = null;

            public int Health { get; set; } // Egészség érték

            public int Hunger { get; set; } // Éhség érték

            public int Thirst { get; set; } // Szomjúság érték

            public int Fatigue { get; set; } // Fáradtság érték
        }
    }




