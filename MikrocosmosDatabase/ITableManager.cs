using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrocosmosDatabase
{
    interface ITableManager<T> {
        bool Add(T obj);
        T GetById(int id);

        IList<T> SearchByFieldNames(string[] fieldNames,object[] values);

        bool Remove(T obj);
        
        bool Update(T obj);
    }
}
