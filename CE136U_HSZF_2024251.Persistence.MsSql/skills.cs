using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class Skills
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }

        [ForeignKey(nameof(Id))]
        public int Charchter_id { get; set; }

        public bool combat { get; set; }
        public bool alchemy { get; set; }
        public bool healing { get; set; }
        public bool tracking { get; set; }
        public bool magic { get; set; }
    }
}
