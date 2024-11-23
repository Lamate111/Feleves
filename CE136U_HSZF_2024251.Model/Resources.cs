using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{
    public class Resources
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }

        [ForeignKey(nameof(Character.id))]
        public int char_id { get; set; }
        [ForeignKey(nameof(Monsters.id))]
        public int monster_id { get; set; }


        [Required]
        public int food { get; set; }
        [Required]
        public int water { get; set; }
        [Required]
        public int weapons { get; set; }
        [Required]
        public int alchemy_ingredients { get; set; }
    }


}
