namespace Restaraunt.Core
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Tool.hbm2ddl;
    using Restaraunt.Entities;

    #endregion

    public static class NHibernateHelperCore
    {
        #region Constants

        public const string RoleAdmin = "Admin";

        public const string RoleUser = "User";

        static readonly ISessionFactory sessionFactory = Fluently.Configure()
                                                                 .Database(MsSqlConfiguration.MsSql2012
                                                                                             .ConnectionString(@"Server=.\SQLEXPRESS; initial catalog= Restaraunt; Integrated Security=SSPI;")
                                                                                             .ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                                                                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                                                 .BuildSessionFactory();

        #endregion

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        public static T SaveOrUpdate<T>(T entity) where T : new()
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }

            return entity;
        }

        public static void DeleteEntities<T>(T entity) where T : class, new()
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }
        }

        public static List<T> GetEntities<T>(Expression<System.Func<T, bool>> expression = null) where T : class, new()
        {
            IList<T> result;
            using (var session = OpenSession())
            {
               var queryOver = expression == null ?
                                 session.QueryOver<T>() :
                                 session.QueryOver<T>().Where(expression);

               result = queryOver.List();
            }

            return (List<T>)result;
        }

        public static T GetSingleOrDefault<T>(Expression<System.Func<T, bool>> expression) where T : class, new()
        {
            T newObject;
            using (var session = OpenSession())
            {
                newObject = session.QueryOver<T>().Where(expression).SingleOrDefault();
            }

            return newObject;
        }
    }
}