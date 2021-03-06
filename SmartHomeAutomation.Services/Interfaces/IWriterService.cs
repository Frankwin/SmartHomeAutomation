﻿using System;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IWriterService<in TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void DeleteByGuid(Guid guid);
    }
}
