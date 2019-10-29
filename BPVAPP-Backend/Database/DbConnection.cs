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
                .Database(MySQLConfiguration.Standard.ConnectionString(""))
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
        public void SaveOrUpdateModel<T>(T model)
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

        public IList<StudentModel> GetStudentsByClass(string classname)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    var model = ses.Query<ClassModel>().Where(i => i.Class.Equals(classname)).FirstOrDefault();

                    if (model == null)
                        return new List<StudentModel>();

                    return ses.Query<StudentModel>().Where(i => i.Class.Equals(model.Class)).ToList();
                }
            }
        }

        public IList<StudentModel> SearchStudent(string query)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    int.TryParse(query,out var studentNumber);

                    return ses.Query<StudentModel>().Where(i =>
                    i.StudentNumber == studentNumber ||
                    i.FirstName.Contains(query) ||
                    i.TussenVoegsel.Contains(query) ||
                    i.LastName.Contains(query) ||
                    i.Class.Contains(query)).ToList();
                }
            }
        }

        public IList<CompanyModel> SearchCompany(string query)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    return ses.Query<CompanyModel>().Where(i =>
                    i.Bedrijfsnaam.Contains(query) ||
                    i.PostCode.Contains(query) ||
                    i.Plaats.Contains(query) ||
                    i.FrameWorks.Contains(query) ||
                    i.Languages.Contains(query)).ToList();
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
