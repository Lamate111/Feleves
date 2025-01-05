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
        void Create(Monster entity);
        void Delete(int id);
        Monster Read(int id);
        void Update(Monster entity);

        public IEnumerable<Monster> GetMonsters();


    }
    public class MonsterService : IMonsterService
    {
        readonly IMonsterDataProvider provider;

        public MonsterService(IMonsterDataProvider provider)
        {
            this.provider = provider;
        }

        public void Create(Monster entity)
        {
            provider.Create(entity);
        }

        public void Delete(int id)
        {
            provider.Delete(id);
        }

        public IEnumerable<Monster> GetMonsters()
        {
            return provider.GetMonsters();
        }

        public Monster Read(int id)
        {
           return provider.Read(id);
        }

        public void Update(Monster entity)
        {
            provider.Update(entity);
        }
    }
}
