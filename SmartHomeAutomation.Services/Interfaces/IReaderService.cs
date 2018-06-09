using System;
using System.Collections.Generic;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IReaderService<out TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByGuid(Guid guid);
        IEnumerable<TEntity> Search(string value);
        PageResult SearchByPage(string value, int pageSize, int pageNumber, string orderBy, string direction);
        PageResult GetByPage(int pageSize, int pageNumber, string orderBy, string direction);
        IEnumerable<TEntity> GetByProperty(string propertyName, object value);
        IEnumerable<TEntity> GetByPropertyIncluding(string propertyName, object value, string[] includeProperties);
        PageResult GetByPropertyByPage(string propertyName, Guid guid, int pageSize, int pageNumber, string orderBy,
            string direction);
    }
}
