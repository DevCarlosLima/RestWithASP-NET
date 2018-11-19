﻿using RestWithASPNET.Models.Base;
using System.Collections.Generic;

namespace RestWithASPNET.Repository.Generic
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long? id);
    }
}