using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrocosmosDatabase
{
     interface ITableManager<T> {
        Task<bool> Add(T obj);
        Task<T> GetById(int id);

        Task<IList<T>> SearchByFieldNames(string[] fieldNames,object[] values);

        Task<bool> Remove(T obj);
        
        Task<bool> Update(T obj);
    }
}
