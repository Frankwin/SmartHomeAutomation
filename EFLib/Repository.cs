using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EFLib
{
    public class Repository<TEntity> where TEntity : class, IObjectWithState
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
            => _dbSet.AsNoTracking().ToList();

        public virtual TEntity FindById(int id)
            => _dbSet.AsNoTracking().SingleOrDefault(Utilities.BuildLambdaForFindByKey<TEntity>(id));

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression)
            => _dbSet.AsNoTracking().Where(expression).ToList();

        public IEnumerable<TEntity> FindBy(string property, object inputValue)
            => FindBy(Utilities.BuildEqualsExpression<TEntity>(property, inputValue));

        public IEnumerable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
            => GetAllIncluding(includeProperties)
                .Where(expression)
                .ToList();

        public IEnumerable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> expression, string[] includeProperties)
        {
            var query = _dbSet.Where(expression).AsNoTracking();
            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            return query.ToList();
        }

        public virtual TEntity GetByIdIncluding(int id, params Expression<Func<TEntity, object>>[] includeProperties)
            => GetAllIncluding(includeProperties).SingleOrDefault(Utilities.BuildLambdaForFindByKey<TEntity>(id));

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate(queryable,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<TEntity> Search(string value)
        {
            var stringProperties = typeof(TEntity).GetProperties().Where(x => x.PropertyType == typeof(string));
            var helpers = stringProperties.Select(x => new Utilities.Helper { PropertyName = x.Name, InputValue = value });
            var expressions = Utilities.BuildContainsExpression<TEntity>(helpers.ToArray());
            var searchExpression = Utilities.CombineExpressions(expressions);

            var result = FindBy(searchExpression);
            return result;
        }

        public IQueryable<TEntity> SearchQuery(string value)
        {
            var stringProperties = typeof(TEntity).GetProperties().Where(x => x.PropertyType == typeof(string));
            var helpers = stringProperties.Select(x => new Utilities.Helper { PropertyName = x.Name, InputValue = value });
            var expressions = Utilities.BuildContainsExpression<TEntity>(helpers.ToArray());
            var searchExpression = Utilities.CombineExpressions(expressions);
            var result = FindBy(searchExpression).AsQueryable();
            return result;
        }

        public IQueryable<TEntity> GetAllQuery() => _dbSet.AsNoTracking().AsQueryable();

        public IQueryable<TEntity> GetAllQueryBy(Expression<Func<TEntity, bool>> expression) => _dbSet.AsNoTracking().Where(expression).AsQueryable();


        public void InsertEntity(TEntity entity)
        {
            _dbSet.Add(entity);
            entity.ObjectState = ObjectState.Added;
        }

        public void UpdateEntity(TEntity entity)
        {
            _dbSet.SafeAttach(entity);
            entity.ObjectState = ObjectState.Modified;
        }

        public void DeleteEntity(TEntity entity)
        {
            _dbSet.SafeAttach(entity);
            entity.ObjectState = ObjectState.Deleted;
        }

        public void DeleteEntity(int id)
        {
            var entity = FindById(id);
            DeleteEntity(entity);
        }
    }
}

