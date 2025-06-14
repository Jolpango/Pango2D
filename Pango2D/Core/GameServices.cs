using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Core
{
    public class GameServices
    {
        private readonly Dictionary<Type, object> services = new();

        public T Get<T>()
        {
            if(services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            throw new Exception($"No service of type {typeof(T)} registered");
        }

        public void Register<T>(T service) where T : class
        {
            services.Add(typeof(T), service);
        }
    }
}
