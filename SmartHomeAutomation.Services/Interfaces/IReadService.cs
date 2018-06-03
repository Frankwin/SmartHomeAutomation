using System;
using System.Collections.Generic;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IReadService<out TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByGuid(Guid guid);
        IEnumerable<TEntity> Search(string value);
        PagingResult SearchByPage(string value, int pageSize, int pageNumber, string orderBy, string direction);
        PagingResult GetByPage(int pageSize, int pageNumber, string orderBy, string direction);
        IEnumerable<TEntity> GetByProperty(string propertyName, object value);
        IEnumerable<TEntity> GetByPropertyIncluding(string propertyName, object value, string[] includeProperties);
        PagingResult GetByPropertyByPage(string propertyName, int id, int pageSize, int pageNumber, string orderBy,
            string direction);
    }
}
