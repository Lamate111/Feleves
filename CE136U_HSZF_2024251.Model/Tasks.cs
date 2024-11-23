using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Model
{


    public class Tasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }
        
        public int char_id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int duration { get; set; }
        [Required]
        public string[,] required_resources { get; set; }
        [Required]
        public string[,] affected_status { get; set; }
        [Required]
        public string[,] reward { get; set; }

    }

}
