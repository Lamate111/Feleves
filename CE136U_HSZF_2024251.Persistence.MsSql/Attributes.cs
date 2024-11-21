using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class Attributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Character_id { get; set; }
        [Required]
        public int health { get; set; }
        [Required]
        public int hunger { get; set; }
        [Required]
        public int thirst { get; set; }
        [Required]
        public int fatigue { get; set; }
    }



}
