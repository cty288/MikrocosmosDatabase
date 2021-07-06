using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using UnityEngine;

namespace MikrocosmosDatabase {
    public class NHibernateHelper:MonoBehaviour {
        private static NHibernateHelper singleton;
        public static NHibernateHelper Singleton {
            get {
                if (singleton == null) {
                    singleton = new NHibernateHelper();
                }

                return singleton;
            }
        }

        private  ISessionFactory sessionFactory;
        private  ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null) {
                    InitializeSessionFactory();
                }

                return sessionFactory;
            }
        }

        void Start() {
            singleton = this;
            InitializeSessionFactory();
        }

        private void InitializeSessionFactory() {
            try
            {
                var configuration = new Configuration();
                configuration.Configure(Application.streamingAssetsPath + "/hibernate.cfg.xml");
                configuration.AddAssembly("MikrocosmosDatabase");
                sessionFactory = configuration.BuildSessionFactory();
            }
            catch (Exception e)
            {
                Debug.Log("Failed to connect to the database. Please check the error below: \n" + e);
            }
        }
        
        
        public  ISession OpenSession()
        {
            
            return SessionFactory.OpenSession();
        }
    }

}
