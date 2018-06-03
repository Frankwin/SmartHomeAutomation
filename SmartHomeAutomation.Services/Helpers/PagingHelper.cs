using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartHomeAutomation.Services.Helpers
{
    public static class PagingHelper
    {
        public static PagingResult GetPageResult<TEntity, TContext>(TContext context, IQueryable<TEntity> query,
            int pageSize, int pageNumber, string orderBy = "", string direction = "asc") where TEntity : class where TContext : DbContext
        {

            var totalCount = query.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            
            if (QueryHelper.PropertyExists<TEntity>(orderBy))
            {
                query = direction.ToLowerInvariant() == "desc" ? query.OrderBy(orderBy + " desc") : query.OrderBy(orderBy);
            }

            var collection = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<dynamic>();

            var result = new PagingResult
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Collection = collection
            };

            return result;
        }
    }
}
