using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;

namespace CE136U_HSZF_2024251.Application
{
    public class AbilitiesService : DataProvider<Abilities>
    {
        public AbilitiesService(AbilitiesDataProvider context)
        {
            _context = context;
        }

        readonly AbilitiesDataProvider _context;

        public void Create(Abilities entity)
        {
            _context.Create(entity);
            
        }

        public void Delete(int id)
        {
            _context.Delete(id);
        }

        public Abilities Read (int id)
        {
            return _context.Read(id);
        }

        public void Update(Abilities entity)
        {
            _context.Update(entity);
        }
    }
}
