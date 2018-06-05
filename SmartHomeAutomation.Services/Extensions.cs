using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Services.Helpers;

namespace SmartHomeAutomation.Services
{
    public static class Extensions
    {
        /// <summary>
        /// Saves the context with the help of `StateHelper`.
        /// </summary>
        /// <param name="context">DBContext</param>
        public static void SaveWithState(this DbContext context)
        {
            context.ApplyStateChanges();
            context.SaveChanges();
        }

        /// <summary>
        /// Attaches the entity to the `IDbSet` only if it is not already attached.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="dbSet">Database Set</param>
        /// <param name="entity">Entity</param>
        public static void SafeAttach<T>(this DbSet<T> dbSet, T entity) where T : class
        {
            if (!CheckIfAttached(dbSet, entity))
                dbSet.Attach(entity);
        }

        private static bool CheckIfAttached<T>(DbSet<T> dbSet, T entity) where T : class
            => dbSet.Local.Any(x => x == entity);
    }
}
