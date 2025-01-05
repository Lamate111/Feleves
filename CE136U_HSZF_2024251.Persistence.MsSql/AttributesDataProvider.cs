using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public interface IAttributesDataProvider
    {
        void Create(Attributes entity);
        void Delete(int id);
        Attributes Read(int id);
        void Update(Attributes entity);
        IEnumerable<Attributes> GetAttributes();

    }
    public class AttributesDataProvider : IAttributesDataProvider
    {
        public AttributesDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        readonly TheWitchAppDataBaseContext _context;

        public void Create(Attributes entity)
        {
            _context.Attributes.Add(entity);
            _context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            var ToBeDeleted = _context.Attributes.Find(id);
            _context.Attributes.Remove(ToBeDeleted);
            _context.SaveChanges();
            
        }

        public Attributes Read(int id)
        {
            return _context.Attributes.Find(id);
        }

        public void Update(Attributes entity)
        {
            _context.Attributes.Update(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Attributes> GetAttributes()
        {
            return _context.Attributes;
        }
    }
}
