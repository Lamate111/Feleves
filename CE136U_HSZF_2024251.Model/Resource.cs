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
        public int? MonsterID_res { get; set; }
        public virtual Monster? Monster { get; set; }

        // Reverse navigation for Tasks.RequiredResources
        public int? RequiredId_res { get; set; }
        public virtual Tasks? RequiredByTask { get; set; }

        // Reverse navigation for Tasks.Reward
        public int? RewardForTaskId_res {  get; set; }
        public virtual Tasks? RewardForTask { get; set; }

        public int? Food { get; set; }
        public int? Water { get; set; }
        public int? Weapons { get; set; }
        public int? AlchemyIngredients { get; set; }
    }
}


