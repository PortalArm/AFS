using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Configurations
{
    public static class ConfigurationGetter
    {
        private static Dictionary<Type, object> _dict { get; } = new Dictionary<Type, object>();
        private static readonly string _namespace = typeof(ConfigurationGetter).Namespace;
        public static EntityTypeConfiguration<T> GetConfig<T>() where T : class
        {
            if (!_dict.ContainsKey(typeof(T)))
                _dict[typeof(T)] = Activator.CreateInstance(Type.GetType(_namespace + "." + typeof(T).Name + "Configuration"));
            
            return (EntityTypeConfiguration<T>)_dict[typeof(T)];
        }
    }
}
