using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public class MonsterService : DataProvider<Monsters>
    {
        readonly MonsterDataProvider provider;
        public void Create(Monsters entity)
        {
            provider.Create(entity);
        }

        public void Delete(int id)
        {
            provider.Delete(id);
        }

        public Monsters Read(int id)
        {
           return provider.Read(id);
        }

        public void Update(Monsters entity)
        {
            provider.Update(entity);
        }
    }
}
