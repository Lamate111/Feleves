using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public interface IMonsterService
    {
        void Create(Monsters entity);
        void Delete(int id);
        Monsters Read(int id);
        void Update(Monsters entity);


    }
    public class MonsterService : IMonsterService
    {
        readonly MonsterDataProvider provider;

        public MonsterService(MonsterDataProvider provider)
        {
            this.provider = provider;
        }

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
