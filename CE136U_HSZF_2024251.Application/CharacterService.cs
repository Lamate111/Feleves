using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public class CharacterService : DataProvider<Character>
    {
        readonly CharacterDataProvider provider;
        public void Create(Character entity)
        {
            provider.Create(entity);
        }

        public void Delete(int id)
        {
            provider?.Delete(id);
        }

        public Character Read(int id)
        {
           return provider.Read(id);
        }

        public void Update(Character entity)
        {
            provider.Update(entity);
        }
    }
}
