﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectNow.Mongo.Models;

namespace ArchitectNow.Mongo.Db
{
    public interface IBaseRepository<T> : IBaseRepository  
     where T : BaseDocument
    {
        Task<bool> DeleteAllAsync();
        Task<List<T>> GetAllAsync();
        Task<T> GetOneAsync(Guid id);
        Task<T> SaveAsync(T item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(T item);
    }

    public interface IBaseRepository : IDisposable
    {
        string CollectionName { get; }
        Task ConfigureIndexes();
        bool HasValidUser();
    }
}
