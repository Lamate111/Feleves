namespace CE136U_HSZF_2024251.Model
{

    public class Character
    {
        public string Name { get; set; }
        public string HealthStatus { get; set; }
        public int Hunger { get; set; }
        public int Thirst { get; set; }
        public int Fatigue { get; set; }
        public Skills Skills { get; set; }
    }

    public class Skills
    {
        public int harci_készség { get; set; }
        public int varázslás { get; set; }
        public int gyógyítás { get; set; }
        public int nyomkövetés { get; set; }
    }


}
