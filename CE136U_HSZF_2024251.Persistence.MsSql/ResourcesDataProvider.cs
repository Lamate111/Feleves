using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class ResourcesDataProvider : DataProvider<Resources>
    {
        public ResourcesDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        TheWitchAppDataBaseContext _context {  get; set; }


        public void Create(Resources entity)
        {
            _context.Resources.Add(entity);
            _context.SaveChanges();
           
        }

        public void Delete(int id)
        {
            var ToBeKilled = _context.Resources.Find(id);
            _context.Resources.Remove(ToBeKilled);
            _context.SaveChanges();
            
        }

        public Resources Read(int id)
        {
            return _context.Resources.Find(id);
        }

        public void Update(Resources entity)
        {
            _context.Resources.Update(entity);
            _context.SaveChanges();

        }
    }
}
