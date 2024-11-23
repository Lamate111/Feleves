﻿using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public class AttributesService : DataProvider<Attributes>
    {
        readonly AttributesDataProvider  provider;
        
        public void Create(Attributes entity)
        {
            provider.Create(entity);
        }

        public void Delete(int id)
        {
            provider.Delete(id);
        }

        public Attributes Read(int id)
        {
           return provider.Read(id);
        }

        public void Update(Attributes entity)
        {
            provider.Update(entity);
        }
    }
}
