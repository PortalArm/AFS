﻿using System;
using System.Collections;
using System.Data.Entity;

namespace ActualFileStorage.DAL.Adapters
{
    public class EFAdapter : IAdapter
    {
        private Type _type;
        private DbContext _context;
        public EFAdapter(DbContext context)
        {
            _context = context;
        }
        public object Add(object entity) => _context.Set(_type).Add(entity);
        public IEnumerable AddRange(IEnumerable entities) => _context.Set(_type).AddRange(entities);
        public object Find(params object[] keyValues)
        {
            if (keyValues == null)
                throw new ArgumentNullException("Key values not specified.");

            return _context.Set(_type).Find(keyValues);
        }
        public object Remove(object entity) => _context.Set(_type).Remove(entity);
        public IEnumerable RemoveRange(IEnumerable entities) => _context.Set(_type).RemoveRange(entities);
        public IAdapter LoadType(Type type) { _type = type; return this; }
        public IEnumerable FindAll() => _context.Set(_type);
    }
}
