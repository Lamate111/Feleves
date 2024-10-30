using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Model
{

    public class Monster
    {
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public Resources Resources { get; set; }
    }

    public class Resources
    {
        public int grifftoll { get; set; }
        public int varázsitalalapanyag { get; set; }
        public int arany {  get; set; }
        public int liderchamu { get; set; }
        public int hús {  get; set; }
        public int farkasbőr { get; set; }


    }


}
