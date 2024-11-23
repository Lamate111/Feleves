using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CE136U_HSZF_2024251.Model
{
    public class Abilities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Character.id))]
        [Required]
        public int char_id { get; set; }

        [Required]
        public bool combat { get; set; }
        [Required]
        public bool magic {  get; set; }
        [Required]
        public bool tracking { get; set; }
        [Required]
        public bool alchemy { get; set; }
        [Required]
        public bool healing { get; set; }


        




    }
}
