using System;
using System.Collections;
using System.Collections.Generic;
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
        public virtual bool Add(T obj)
        {
            using (ISession session = NHibernateHelper.Singleton.OpenSession())
            {
                using (ITransaction traansaction = session.BeginTransaction())
                {
                    try {
                        session.Save(obj);
                        traansaction.Commit();
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
        public virtual IList<T> SearchByFieldNames(string[] fieldNames, object[] values)
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
                        ICriteria criteria = session.CreateCriteria(typeof(object));

                        for (int i = 0; i < fieldNames.Length; i++)
                        {
                            criteria.Add(Restrictions.Eq(fieldNames[i], values[i]));
                        }


                        return criteria.List<T>();
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
        public virtual IList<T> SearchByFieldName(string fieldName, object value)
        {
            try
            {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ICriteria criteria = session.CreateCriteria(typeof(object));
                        
                        criteria.Add(Restrictions.Eq(fieldName, value));

                        return criteria.List<T>();
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
        public virtual T SearchByFieldNamesUniqueResult(string[] fieldNames, object[] values)
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
                        ICriteria criteria = session.CreateCriteria(typeof(object));

                        for (int i = 0; i < fieldNames.Length; i++)
                        {
                            criteria.Add(Restrictions.Eq(fieldNames[i], values[i]));
                        }


                        return criteria.UniqueResult<T>();
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
        public virtual T SearchByFieldNameUniqueResult(string fieldName, object value)
        {

            try
            {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ICriteria criteria = session.CreateCriteria(typeof(object));

                        criteria.Add(Restrictions.Eq(fieldName, value));


                        return criteria.UniqueResult<T>();
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
        public virtual T GetById(int id)
        {
            try {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction traansaction = session.BeginTransaction())
                    {
                        T obj = session.Get<T>(id);
                        traansaction.Commit();
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
        public virtual bool Remove(T obj)
        {
            try {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction traansaction = session.BeginTransaction())
                    {
                        session.Delete(obj);
                        traansaction.Commit();
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
        public virtual bool Update(T obj)
        {
            try {
                using (ISession session = NHibernateHelper.Singleton.OpenSession())
                {
                    using (ITransaction traansaction = session.BeginTransaction())
                    {
                        session.Update(obj);
                        traansaction.Commit();
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

