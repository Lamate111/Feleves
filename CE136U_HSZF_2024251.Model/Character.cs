namespace CE136U_HSZF_2024251.Model
{

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string health_status { get; set; }
       

        public string[] abilities { get; set; }
        public Resources resources { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public int health { get; set; }
        public int hunger { get; set; }
        public int thirst { get; set; }
        public int fatigue { get; set; }
    }

    public class Resources
    {
        public int food { get; set; }
        public int water { get; set; }
        public int weapons { get; set; }
        public int alchemy_ingredients { get; set; }
    }


}
