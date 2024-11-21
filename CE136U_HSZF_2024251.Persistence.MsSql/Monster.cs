using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{


    public class Monster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        public int difficulty { get; set; }

        [Required,StringLength(100)]
        public string required_ability { get; set; }
        public virtual ICollection<Loot> loot { get; set; }

        public Monster()
        {
            loot = new HashSet<Loot>();
        }
    }
}
