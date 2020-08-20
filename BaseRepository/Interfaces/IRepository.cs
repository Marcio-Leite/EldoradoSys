﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<IEnumerable<TEntity>> Query(string where = null, object parameters = null);
        Task<TEntity> GetById(Guid id);
        void Update(TEntity obj);
        void Delete(TEntity obj);
    }
}
