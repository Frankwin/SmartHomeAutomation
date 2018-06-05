using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Domain.Interfaces;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services
{
    public class BaseService<TEntity, TContext> : IReaderService<TEntity>, IWriterService<TEntity>
        where TEntity : class, IObjectWithState
        where TContext : DbContext, new()
    {
        protected string ConnectionString { get; }

        public BaseService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                return repo.GetAll();
            }
        }

        public IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                return repo.GetAllIncluding(includeProperties).ToList();
            }
        }

        public IEnumerable<TEntity> GetByProperty(string propertyName, object value)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                return repo.FindBy(propertyName, value);
            }
        }

        public IEnumerable<TEntity> GetByPropertyIncluding(string propertyName, object value, string[] includeProperties)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                var expression = Utilities.BuildEqualsExpression<TEntity>(propertyName, value);
                return repo.FindByIncluding(expression, includeProperties);
            }
        }

        public IEnumerable<TEntity> Search(string value)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                return repo.Search(value);
            }
        }

        public PageResult SearchByPage(string value, int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                var query = repo.SearchQuery(value);
                return PagingHelper.GetPageResult(context, query, pageSize, pageNumber, orderBy, direction);
            }
        }

        public PageResult GetByPage(int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                var query = repo.GetAllQuery();
                return PagingHelper.GetPageResult(context, query, pageSize, pageNumber, orderBy, direction);
            }
        }

        public PageResult GetByPropertyByPage(string propertyName, int id, int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                var lambda = Utilities.BuildEqualsExpression<TEntity>(propertyName, id);
                var query = repo.GetAllQueryBy(lambda);
                return PagingHelper.GetPageResult(context, query, pageSize, pageNumber, orderBy, direction);
            }
        }

        public TEntity GetByGuid(Guid guid)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                return repo.FindByGuid(guid);
            }
        }

        public void Insert(TEntity entity)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                repo.InsertEntity(entity);
                context.SaveWithState();
            }
        }

        public virtual void Update(TEntity entity)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                repo.UpdateEntity(entity);
                context.SaveWithState();
            }
        }

        public virtual void DeleteByGuid(Guid guid)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                repo.DeleteEntity(guid);
                context.SaveWithState();
            }
        }

        private static TContext CreateContext(string connectionString)
        {
            var context = new TContext();
            context.Database.GetDbConnection().ConnectionString = connectionString;
            return context;
        }
    }
}

