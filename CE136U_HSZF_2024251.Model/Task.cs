using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Model
{

    public class Task
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Impact { get; set; }
        public Resourcerequirements ResourceRequirements { get; set; }
    }

    public class Resourcerequirements
    {
        public int gyógynövény { get; set; }
        public int víz { get; set; }
        public int fegyver { get; set; }
        public int alkímiaialapanyag {  get; set; }
        public int varázsital {  get; set; }
        public int kovácseszköz {  get; set; }

    }


}
