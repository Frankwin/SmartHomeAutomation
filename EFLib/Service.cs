﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EFLib
{
    public class Service<TEntity, TContext> : IReadService<TEntity>, IWriteService<TEntity>
        where TEntity : class, IObjectWithState 
        where TContext : DbContext, new()
    {
        protected string ConnectionString { get; }

        public Service(string connectionString)
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

        public PagingResult SearchByPage(string value, int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                var query = repo.SearchQuery(value);
                return PagingHelper.GetPageResult(context, query, pageSize, pageNumber, orderBy, direction);
            }
        }

        public PagingResult GetByPage(int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                var query = repo.GetAllQuery();
                return PagingHelper.GetPageResult(context, query, pageSize, pageNumber, orderBy, direction);
            }
        }

        public PagingResult GetByPropertyByPage(string propertyName, int id, int pageSize, int pageNumber, string orderBy, string direction)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                var lambda = Utilities.BuildEqualsExpression<TEntity>(propertyName, id);
                var query = repo.GetAllQueryBy(lambda);
                return PagingHelper.GetPageResult(context, query, pageSize, pageNumber, orderBy, direction);
            }
        }

        public TEntity GetById(int id)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                return repo.FindById(id);
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

        public virtual void DeleteById(int id)
        {
            using (var context = CreateContext(ConnectionString))
            {
                var repo = new Repository<TEntity>(context);
                repo.DeleteEntity(id);
                context.SaveWithState();
            }
        }

        private static TContext CreateContext(string connectionString)
        {
            var context = new TContext();
            context.Database.Connection.ConnectionString = connectionString;
            return context;
        }
    }
}

