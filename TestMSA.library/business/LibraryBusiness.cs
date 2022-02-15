using br.com.ussolucoes.persistence;
using br.com.ussolucoes.persistence.Dao;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMSA.library.business
{
    [Serializable]
    public abstract class LibraryBusiness<T>
    {
        private PersistenciaDao<T> objPersistenciaDao;
        private readonly IConfiguration objConfiguration;

        public LibraryBusiness()
        {

            string cfgNhibernate = System.Configuration.ConfigurationManager.AppSettings["CFG_TestMSA.library"];
            if (cfgNhibernate == null)
            {

                var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                       .AddEnvironmentVariables();

                objConfiguration = builder.Build();

                cfgNhibernate = objConfiguration["NHibernate:CFG_TestMSA.library"];
            }
            objPersistenciaDao = new PersistenciaDao<T>(cfgNhibernate, "TestMSA.library");
        }

        public Boolean SaveOrUpdate(T obj)
        {
            objPersistenciaDao.SaveOrUpdate(obj);
            return true;
        }
        public virtual Boolean Update(T obj)
        {
            objPersistenciaDao.Update(obj);
            return true;
        }
        public virtual Boolean Save(T obj)
        {
            objPersistenciaDao.Save(obj);
            return true;
        }
        public async virtual Task<Boolean> SaveAsync(T obj)
        {
            using (ITransaction transaction = GetSession().BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                await GetSession().SaveAsync(obj);
                await transaction.CommitAsync();

            }

            return true;
        }
        public void Refresh(T obj)
        {
            objPersistenciaDao.Refresh(obj);
        }
        public virtual Boolean Delete(T obj)
        {
            objPersistenciaDao.Delete(obj);
            return true;
        }
        public virtual T GetById(Object parId)
        {
            return objPersistenciaDao.GetById(parId);
        }
        public List<T> GetAll()
        {
            return objPersistenciaDao.GetAll();
        }
        public List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            return objPersistenciaDao.GetByExample(exampleInstance, propertiesToExclude);
        }
        public T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            return objPersistenciaDao.GetUniqueByExample(exampleInstance, propertiesToExclude);
        }
        public List<T> ExecCriteria(List<ICriterion> parExpressions)
        {
            return objPersistenciaDao.ExecCriteria(parExpressions);

        }
        public T ExecCriteriaUnique(List<ICriterion> parExpressions)
        {
            return objPersistenciaDao.ExecCriteriaUnique(parExpressions);

        }

        public T ExecQueryUnique(string parHQLquery)
        {
            return (T)this.objPersistenciaDao.ExecQueryUnique(parHQLquery);
        }
        public List<T> ExecQuery(string parHQLquery)
        {
            return (List<T>)this.objPersistenciaDao.ExecQuery(parHQLquery);
        }
        public IQuery ExecGenericQuery(string parHQLquery)
        {
            return this.objPersistenciaDao.ExecGenericQuery(parHQLquery);
        }
        public ISession GetSession()
        {
            return this.objPersistenciaDao.Session;
        }
        public IStatelessSession OpenStatelessSession()
        {
            return this.objPersistenciaDao.OpenStatelessSession();
        }
        public List<T> GetLastNew(int Max)
        {


            ICriteria criteria = GetSession().CreateCriteria(typeof(T));
            criteria.SetFirstResult(0);
            criteria.SetMaxResults(Max);
            criteria.AddOrder(Order.Desc("Created"));

            return (System.Collections.Generic.List<T>)criteria.List<T>();

        }
        public int Count()
        {
            return objPersistenciaDao.Count();
        }
        

        public PagedList<T> PagedList(int pageIndex, int pageSize, string orderProperty = "", OrderResult orderResult = OrderResult.Asc, List<ICriterion> parExpressions = null) //where T : class
        {
            ICriteria objCriteria = GetSession().CreateCriteria(typeof(T));
            if (parExpressions != null)
                foreach (ICriterion ex in parExpressions)
                {
                    objCriteria.Add(ex);
                }
            if (orderProperty != string.Empty)
            {
                if (orderResult == OrderResult.Asc)
                    objCriteria.AddOrder(Order.Asc(orderProperty));
                else
                    objCriteria.AddOrder(Order.Desc(orderProperty));
            }

            if (pageIndex < 0)
                pageIndex = 0;

            var countCrit = (ICriteria)objCriteria.Clone();
            countCrit.ClearOrders(); // so we don't have missing group by exceptions

            var results = GetSession().CreateMultiCriteria()
                .Add<long>(countCrit.SetProjection(Projections.RowCountInt64()))
                .Add<T>(objCriteria.SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize))
                .List();

            var totalCount = ((IList<long>)results[0])[0];

            return new PagedList<T>((IList<T>)results[1], totalCount, pageIndex, pageSize);

        }

    }
    public enum OrderResult
    {
        Asc,
        Desc
    }
}
