using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public interface IResourcesService
    {
        void Create(Resource entity);
        void Delete(int id);
        Resource Read(int id);
        void Update(Resource entity);
        public IEnumerable<Resource> GetResources();

    }
    public class ResourcesServices : IResourcesService
    {
        readonly IResourcesDataProvider _context;

        public ResourcesServices(IResourcesDataProvider context)
        {
            _context = context;
        }

        public void Create(Resource entity)
        {
            _context.Create(entity);
            
        }

        public void Delete(int id)
        {
            _context.Delete(id);
        }

        public IEnumerable<Resource> GetResources()
        {
            return _context.GetResources();
        }

        public Resource Read(int id)
        {
            return _context.Read(id);
        }

        public void Update(Resource entity)
        {
            _context.Update(entity);
        }
    }
}
