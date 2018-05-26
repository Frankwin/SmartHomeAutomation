using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace EFLib
{
    public class PagingResult
    {
        public int TotalCount { get; set; }
        public double TotalPages { get; set; }
        public List<dynamic> Collection { get; set; }
    }

    public static class PagingHelper
    {
        public static PagingResult GetPageResult<TEntity, TContext>(TContext context, IQueryable<TEntity> query,
            int pageSize, int pageNumber, string orderBy = "", string direction = "asc") where TEntity : class where TContext : DbContext
        {

            var totalCount = query.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var thisType = typeof(TEntity);

            if (QueryHelper.PropertyExists<TEntity>(orderBy))
            {
                query = direction.ToLowerInvariant() == "desc" ? query.OrderBy(orderBy + " desc") : query.OrderBy(orderBy);
            }
            else
            {
                var pk = GetPrimaryKeyNames<TEntity, TContext>(context);
                query = query.OrderByDescending(x => pk.FirstOrDefault());
            }

            var collection = query.ToList().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<dynamic>();

            var result = new PagingResult
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Collection = collection
            };

            return result;

        }

        // this probably needs some love
        public static IEnumerable<string> GetPrimaryKeyNames<TEntity, TContext>(TContext context)
            where TEntity : class
            where TContext : DbContext
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var entityType = objectContext.CreateObjectSet<TEntity>().EntitySet.ElementType;
            return entityType.KeyProperties.Select(p => p.Name);
        }
    }
}
