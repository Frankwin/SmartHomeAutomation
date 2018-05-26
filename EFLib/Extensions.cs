using System.Data.Entity;
using System.Linq;

namespace EFLib
{
    public static class Extensions
    {
        /// <summary>
        /// Saves the context with the help of `StateHelper`.
        /// </summary>
        /// <param name="context"></param>
        public static void SaveWithState(this DbContext context)
        {
            context.ApplyStateChanges();
            context.SaveChanges();
        }

        /// <summary>
        /// Attaches the entity to the `IDbSet` only if it is not already attached.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="entity"></param>
        public static void SafeAttach<T>(this IDbSet<T> dbSet, T entity) where T : class
        {
            if (!CheckIfAttached(dbSet, entity))
                dbSet.Attach(entity);
        }

        private static bool CheckIfAttached<T>(IDbSet<T> dbSet, T entity) where T : class
            => dbSet.Local.Any(x => x == entity);
    }
}
