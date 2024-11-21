using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class Loot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Monster_id { get; set; }
        [Required]
        public int weapons { get; set; }
        [Required]
        public int food { get; set; }
        [Required]
        public int water { get; set; }
        [Required]
        public int alchemy_ingredients { get; set; }
    }



}
