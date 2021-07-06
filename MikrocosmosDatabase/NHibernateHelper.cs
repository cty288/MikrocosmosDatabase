using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using UnityEngine;

namespace MikrocosmosDatabase {
    public class NHibernateHelper {
        public static string filePath ;
       
        private static ISessionFactory sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    try
                    {
                        var configuration = new Configuration();
                        configuration.Configure(filePath + "/hibernate.cfg.xml");
                        configuration.AddAssembly("MikrocosmosDatabase");
                        sessionFactory = configuration.BuildSessionFactory();
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Failed to connect to the database. Please check the error below: \n" + e);
                    }

                }

                return sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            
            return SessionFactory.OpenSession();
        }
    }

}
