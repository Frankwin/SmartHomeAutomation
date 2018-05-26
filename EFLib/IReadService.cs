using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EFLib
{
    public interface IReadService<out TEntity> where TEntity : class, IObjectWithState
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        IEnumerable<TEntity> Search(string value);
        PagingResult SearchByPage(string value, int pageSize, int pageNumber, string orderBy, string direction);
        PagingResult GetByPage(int pageSize, int pageNumber, string orderBy, string direction);
        IEnumerable<TEntity> GetByProperty(string propertyName, object value);
        IEnumerable<TEntity> GetByPropertyIncluding(string propertyName, object value, string[] includeProperties);

        PagingResult GetByPropertyByPage(string propertyName, int id, int pageSize, int pageNumber, string orderBy,
            string direction);
    }
}