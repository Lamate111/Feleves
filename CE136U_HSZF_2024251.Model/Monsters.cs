using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CE136U_HSZF_2024251.Model
{

    public class Monsters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int difficulty { get; set; }
        [Required]
        public string required_ability { get; set; }
  
        public Monsters()
        {
            loot = new HashSet<Resources>();
        }
        public virtual ICollection<Resources> loot {  get; set; }
    }

    //public class Loot
    //{
    //    public int weapons { get; set; }
    //    public int food { get; set; }
    //    public int water { get; set; }
    //    public int alchemy_ingredients { get; set; }
    





}
