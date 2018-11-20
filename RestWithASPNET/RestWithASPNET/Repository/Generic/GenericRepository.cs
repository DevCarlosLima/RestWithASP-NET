﻿using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models.Base;
using RestWithASPNET.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MySQLContext _context;
        public DbSet<T> dataSet { get; set; }

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataSet = context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataSet.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return item;
        }

        public void Delete(long id)
        {
            var result = dataSet.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if (result != null)
                {
                    dataSet.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            var result = dataSet.Any(p => p.Id.Equals(id));
            return result;
        }

        public List<T> FindAll()
        {
            return dataSet.ToList();
        }

        public T FindById(long id)
        {
            return dataSet.FirstOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = dataSet.SingleOrDefault(p => p.Id.Equals(item.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return item;
        }
    }
}
