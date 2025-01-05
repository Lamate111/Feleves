using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public interface IResourcesDataProvider
    {
        void Create(Resource entity);
        void Delete(int id);
        Resource Read (int id);
        void Update(Resource entity);
        IEnumerable<Resource> GetResources();

    }
    public class ResourcesDataProvider : IResourcesDataProvider
    {
        public ResourcesDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

         readonly TheWitchAppDataBaseContext _context; 


        public void Create(Resource entity)
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

        public Resource Read(int id)
        {
            return _context.Resources.Find(id);
        }

        public void Update(Resource entity)
        {
            _context.Resources.Update(entity);
            _context.SaveChanges();

        }

        public IEnumerable<Resource> GetResources()
        {
           return _context.Resources;
        }
    }
}
