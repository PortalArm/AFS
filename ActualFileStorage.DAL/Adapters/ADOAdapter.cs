﻿using System;
using System.Collections;
using System.Data.Common;

namespace ActualFileStorage.DAL.Adapters
{
    public class ADOAdapter : IAdapter
    {
        DbConnection _connection;
        private string _type;
        //private static void DisplayData(System.Data.DataTable table)
        //{
        //    foreach (System.Data.DataRow row in table.Rows)
        //    {
        //        foreach (System.Data.DataColumn col in table.Columns)
        //        {
        //            Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
        //        }
        //        Console.WriteLine("============================");
        //    }
        //}

        public ADOAdapter(DbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _connection.Close();

        }
        public object Add(object entity)
        {
            _connection.Open();
            _connection.Close();
            throw new NotImplementedException();
        }

        public IEnumerable AddRange(IEnumerable entities)
        {
            _connection.Open();
            _connection.Close();
            throw new NotImplementedException();
        }

        public object Find(params object[] keyValues)
        {
            _connection.Open();
            _connection.Close();
            throw new NotImplementedException();
        }

        public object Remove(object entity)
        {
            _connection.Open();
            _connection.Close();
            throw new NotImplementedException();
        }

        public IEnumerable RemoveRange(IEnumerable entities)
        {
            _connection.Open();
            _connection.Close();
            throw new NotImplementedException();
        }

        public IAdapter LoadType(Type type)
        {
            _type = SimpleMapper.Get(type);
            return this;
        }

        public IEnumerable FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
