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
        void Create(Resources entity);
        void Delete(int id);
        Resources Read(int id);
        void Update(Resources entity);

    }
    public class ResourcesServices : IResourcesService
    {
        readonly ResourcesDataProvider _context;

        public ResourcesServices(ResourcesDataProvider context)
        {
            _context = context;
        }

        public void Create(Resources entity)
        {
            _context.Create(entity);
            
        }

        public void Delete(int id)
        {
            _context.Delete(id);
        }

        public Resources Read(int id)
        {
            return _context.Read(id);
        }

        public void Update(Resources entity)
        {
            _context.Update(entity);
        }
    }
}
