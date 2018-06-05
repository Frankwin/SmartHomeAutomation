using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;
using SmartHomeAutomation.Services.Helpers;

namespace SmartHomeAutomation.Services
{
    public class Repository<TEntity> where TEntity : class, IObjectWithState
    {
        protected DbContext Context;
        protected DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
            => DbSet.AsNoTracking().ToList();

        public virtual TEntity FindByGuid(Guid id)
            => DbSet.AsNoTracking().SingleOrDefault(Utilities.BuildLambdaForFindByKey<TEntity>(id));

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression)
            => DbSet.AsNoTracking().Where(expression).ToList();

        public IEnumerable<TEntity> FindBy(string property, object inputValue)
            => FindBy(Utilities.BuildEqualsExpression<TEntity>(property, inputValue));

        public IEnumerable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
            => GetAllIncluding(includeProperties)
                .Where(expression)
                .ToList();

        public IEnumerable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> expression, string[] includeProperties)
        {
            var query = DbSet.Where(expression).AsNoTracking();
            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            return query.ToList();
        }

        public virtual TEntity GetByIdIncluding(Guid guid, params Expression<Func<TEntity, object>>[] includeProperties)
            => GetAllIncluding(includeProperties).SingleOrDefault(Utilities.BuildLambdaForFindByKey<TEntity>(guid));

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = DbSet.AsNoTracking();

            return includeProperties.Aggregate(queryable,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<TEntity> Search(string value)
        {
            var stringProperties = typeof(TEntity).GetProperties().Where(x => x.PropertyType == typeof(string));
            var helpers = stringProperties.Select(x => new UtilityHelper { PropertyName = x.Name, InputValue = value });
            var expressions = Utilities.BuildContainsExpression<TEntity>(helpers.ToArray());
            var searchExpression = Utilities.CombineExpressions(expressions);

            var result = FindBy(searchExpression);
            return result;
        }

        public IQueryable<TEntity> SearchQuery(string value)
        {
            var stringProperties = typeof(TEntity).GetProperties().Where(x => x.PropertyType == typeof(string));
            var helpers = stringProperties.Select(x => new UtilityHelper { PropertyName = x.Name, InputValue = value });
            var expressions = Utilities.BuildContainsExpression<TEntity>(helpers.ToArray());
            var searchExpression = Utilities.CombineExpressions(expressions);
            var result = FindBy(searchExpression).AsQueryable();
            return result;
        }

        public IQueryable<TEntity> GetAllQuery() => DbSet.AsNoTracking().AsQueryable();

        public IQueryable<TEntity> GetAllQueryBy(Expression<Func<TEntity, bool>> expression) => DbSet.AsNoTracking().Where(expression).AsQueryable();


        public void InsertEntity(TEntity entity)
        {
            DbSet.Add(entity);
            entity.ObjectState = ObjectState.Added;
        }

        public void UpdateEntity(TEntity entity)
        {
            DbSet.SafeAttach(entity);
            entity.ObjectState = ObjectState.Modified;
        }

        public void DeleteEntity(TEntity entity)
        {
            DbSet.SafeAttach(entity);
            entity.ObjectState = ObjectState.Deleted;
        }

        public void DeleteEntity(Guid guid)
        {
            var entity = FindByGuid(guid);
            DeleteEntity(entity);
        }
    }
}

