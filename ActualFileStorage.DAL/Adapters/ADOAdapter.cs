using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace ActualFileStorage.DAL.Adapters
{
    public class ADOAdapter : IAdapter
    {
        private DbConnection _connection;
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


        // ?
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
        public IAdapter LoadType<T>() => LoadType(typeof(T));


        public IEnumerable FindAll()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable FindByPred<T>(Expression<Func<T, bool>> expr) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> ExecuteSql<TElement>(string sql, params SqlParameter[] pars)
        {
            throw new NotImplementedException();
        }

        public IEnumerable ExecuteSqlAsTracked(string sql, params SqlParameter[] pars)
        {
            throw new NotImplementedException();
        }
    }
}
