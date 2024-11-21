using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{


    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [Required]
        public string health_status { get; set; }

        public Character()
        {
            attributes = new HashSet<Attribute>();
            skills = new HashSet<Skills>();
            resources = new HashSet<Resources>();

        }


        public virtual ICollection<Attribute> attributes { get; set; }
        public virtual ICollection<Skills> skills { get; set; }
        public virtual ICollection<Resources> resources { get; set; }
    }
}
