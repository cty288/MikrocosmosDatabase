using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using UnityEngine;

namespace MikrocosmosDatabase {
    public class TableBaseManager<T> : ITableManager<T>
    {
        /// <summary>
        /// Insert a data to the table. Returns whether insert success
        /// </summary>
        /// <param name="obj">The inserted data object must have a mapping with NHibernate</param>
        public async virtual Task<bool> Add(T obj)
        {
            using (ISession session = NHibernateHelper.Singleton.OpenSession())
            {
                using (ITransaction traansaction = session.BeginTransaction())
                {
                    try {
                        await session.SaveAsync(obj);
                        await traansaction.CommitAsync();
                        return true;
                    }
                    catch (Exception e) {
                        Debug.Log("Failed to insert into the table!\n"+e);
                        return false;
                    }
   
                }
            }
        }

        /// <summary>
        /// Search a data from the table, given restrictions
        /// </summary>
        /// <param name="fieldNames">The name of the field in the corresponding Models class of the table</param>
        /// <param name="values">The value of the field to check</param>
        /// <returns>A list of result. Null if not found or has error</returns>
        public virtual async Task<IList<T>> SearchByFieldNames(string[] fieldNames, object[] values)
        {
            if (fieldNames.Length != values.Length) {
                Debug.Log("Failed to search by field names, because the lengths of " +
                          "field names and search values are not equal!");
                return null;
            }

            try {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ICriteria criteria = session.CreateCriteria(typeof(T));

                        for (int i = 0; i < fieldNames.Length; i++)
                        {
                            criteria.Add(Restrictions.Eq(fieldNames[i], values[i]));
                        }


                        return await criteria.ListAsync<T>();
                    }
                }
            }
            catch (Exception e) {
                Debug.Log("Failed to search by field names!\n" + e);
                return null;
            }
        }
        /// <summary>
        /// Search a data from the table, given a single restriction
        /// </summary>
        /// <param name="fieldName">The name of the field in the corresponding Models class of the table</param>
        /// <param name="value">The value of the field to check</param>
        /// <returns>A list of result. Null if not found or has error</returns>
        public virtual async Task<IList<T>> SearchByFieldName(string fieldName, object value)
        {
            try
            {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ICriteria criteria = session.CreateCriteria(typeof(T));
                        
                        criteria.Add(Restrictions.Eq(fieldName, value));

                        return await criteria.ListAsync<T>();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("Failed to search by field names!\n" + e);
                return null;
            }
        }
        /// <summary>
        /// Search a UNIQUE data from the table, given restrictions. 
        /// </summary>
        /// <param name="fieldNames">The name of the field in the corresponding Models class of the table</param>
        /// <param name="values">The value of the field to check</param>
        /// <returns>A list of result. Null if not found or has error</returns>
        public virtual async Task<T> SearchByFieldNamesUniqueResult(string[] fieldNames, object[] values)
        {
            if (fieldNames.Length != values.Length)
            {
                Debug.Log("Failed to search by field names, because the lengths of " +
                          "field names and search values are not equal!");
                return default(T);
            }

            try
            {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ICriteria criteria = session.CreateCriteria(typeof(T));

                        for (int i = 0; i < fieldNames.Length; i++)
                        {
                            criteria.Add(Restrictions.Eq(fieldNames[i], values[i]));
                        }


                        return await criteria.UniqueResultAsync<T>();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("Failed to search by field names!\n" + e);
                return default(T);
            }
        }


        /// <summary>
        /// Search a UNIQUE data from the table, given a single restriction. 
        /// </summary>
        /// <param name="fieldName">The name of the field in the corresponding Models class of the table</param>
        /// <param name="value">The value of the field to check</param>
        /// <returns>A list of result. Null if not found or has error</returns>
        public virtual async Task<T> SearchByFieldNameUniqueResult(string fieldName, object value)
        {

            try
            {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ICriteria criteria = session.CreateCriteria(typeof(T));

                        criteria.Add(Restrictions.Eq(fieldName, value));


                        return await criteria.UniqueResultAsync<T>();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("Failed to search by field names!\n" + e);
                return default(T);
            }
        }

        /// <summary>
        /// Get a data by id from the database
        /// </summary>
        /// <param name="id">The id of the data.</param>
        /// <returns></returns>
        public virtual async Task<T> GetById(int id)
        {
            try {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction traansaction = session.BeginTransaction())
                    {
                        T obj = await session.GetAsync<T>(id);
                        await traansaction.CommitAsync();
                        return obj;
                    }
                }
            }
            catch (Exception e) {
                Debug.Log("Failed to get by id!\n" + e);
                return default(T);
            }
            
        }
        /// <summary>
        /// Remove a data from the table
        /// </summary>
        /// <param name="obj">The object to remove. Must have a mapping in NHibernate</param>
        /// <returns></returns>
        public virtual async Task<bool> Remove(T obj)
        {
            try {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction traansaction = session.BeginTransaction())
                    {
                        await session.DeleteAsync(obj);
                        await traansaction.CommitAsync();
                        return true;
                    }
                }
            }
            catch (Exception e) {
                Debug.Log("Failed to remove!\n" + e);
                return false;
            }
            
        }

        /// <summary>
        /// Update a data from the table. Returns whether update success. 
        /// </summary>
        /// <param name="obj">The object to update. Must have a mapping in NHibernate</param>
        /// <returns></returns>
        public virtual async Task<bool> Update(T obj)
        {
            try {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction traansaction = session.BeginTransaction())
                    {
                        await session.UpdateAsync(obj);
                        await traansaction.CommitAsync();
                        return true;
                    }
                }
            }
            catch (Exception e) {
                Debug.Log("Failed to update!\n" + e);
                return false;
            }
            
        }

    }
}

