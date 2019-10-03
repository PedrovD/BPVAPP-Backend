using System.Collections.Generic;
using FluentNHibernate.Cfg;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using BPVAPP_Backend.Database.Models;
using NHibernate.Tool.hbm2ddl;
using System.Linq;

namespace BPVAPP_Backend.Database
{
    public class DbConnection
    {
        private readonly ISessionFactory sessionFactory;

        private static ISessionFactory CreateSessionFactory()
        {
            var config = Fluently
                .Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString($"Server=localhost; Port=3306; Database=BPV_BACKEND; Uid=root; Pwd=;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CompanyMapping>())
                .BuildConfiguration();

            var exporter = new SchemaUpdate(config);
            exporter.Execute(false, true);

            return config.BuildSessionFactory(); ;
        }

        public DbConnection()
        {
            sessionFactory = CreateSessionFactory();
        }

        /// <summary>
        /// Simple methode to get all records from a table, you can use this method on all tables because its
        /// a genereric function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllModels<T>()
        {
            /// example use age
            /// var db = new DbConnection()
            /// List<CarModel> cars = db.GetAllModels<Cars>();
            /// List<PlaneModel> cars = db.GetAllModels<Plane>();
            /// 
            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    return session.Query<T>().ToList();
                }
            }
        }

        /// <summary>
        /// Simple methode to add any typ of model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void AddModel<T>(T model)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    ses.SaveOrUpdate(model);
                    trans.Commit();
                }
            }
        }

        /// <summary>
        /// Simple methode to remove any type of model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void DeleteModel<T>(T model)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    ses.Delete(model);
                    trans.Commit();
                }
            }
        }

        public CompanyModel GetCompanyById(int id)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    return ses.Query<CompanyModel>().Where(i => i.Id == id).FirstOrDefault();
                }
            }
        }
        public StudentModel GetStudentById(int id)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    return ses.Query<StudentModel>().Where(i => i.Id == id).FirstOrDefault();
                }
            }
        }
        public ClassModel GetClassById(int id)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    return ses.Query<ClassModel>().Where(i => i.Id == id).FirstOrDefault();
                }
            }
        }
    }
}
