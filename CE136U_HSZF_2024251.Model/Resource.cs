using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }

        public int HeroId { get; set; }

        [ForeignKey("HeroId")]
        public virtual Hero? Hero { get; set; }


        public virtual Monster? Monster { get; set; }

        // Reverse navigation for Tasks.RequiredResources
        [InverseProperty(nameof(Tasks.RequiredResources))]
        public virtual Tasks? RequiredByTask { get; set; }

        // Reverse navigation for Tasks.Reward
        [InverseProperty(nameof(Tasks.Reward))]
        public virtual Tasks? RewardForTask { get; set; }

        public int? Food { get; set; }
        public int? Water { get; set; }
        public int? Weapons { get; set; }
        public int? AlchemyIngredients { get; set; }
    }
}


