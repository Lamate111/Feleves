using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace CE136U_HSZF_2024251.Model
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }

        [ForeignKey("HeroId")]
        public int? HeroID_res { get; set; }
        public virtual Hero? Hero { get; set; }

        [ForeignKey("MonsterId")]
        public int? LootID { get; set; }
        public virtual Monster? Monster { get; set; }

        // Reverse navigation for Tasks.RequiredResources
        [ForeignKey("TaskId")]
        public int? RequiredResourcesId { get; set; }
        public virtual Tasks? RequiredResourcesTask { get; set; }

        // Reverse navigation for Tasks.Reward
        [ForeignKey("TaskId")]
        public int? RewardID { get; set; }
        public virtual Tasks? RewardTask { get; set; }

        public int? Food { get; set; }
        public int? Water { get; set; }
        public int? Weapons { get; set; }
        public int? Alchemy_ingredients { get; set; }

        public override string ToString()
        {
            return $"Food :{Food}\n" +
                   $"Water : {Water}" +
                   $"Weapons : {Weapons}" +
                   $"Alchemy_Ingredients {Alchemy_ingredients}";
        }
    }
}


