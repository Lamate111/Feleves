using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Model
{

    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string health_status { get; set; }

 

        public Character()
        {
            resources = new HashSet<Resources>();
            attributes = new HashSet<Attributes>();
            abilities = new HashSet<Abilities>();
            tasks = new HashSet<Tasks>();
        }
        public virtual ICollection<Resources> resources { get; set; }

        [Required]
        public virtual ICollection<Attributes> attributes { get; set; }
        [Required]
        public virtual ICollection<Abilities> abilities { get; set; }
        public virtual ICollection<Tasks> tasks { get; set; }

    }


}
