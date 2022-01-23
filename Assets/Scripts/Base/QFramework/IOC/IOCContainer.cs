using System;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public class IOCContainer 
    {
        Dictionary<Type, object> m_Instances = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            var key = typeof(T);

            if (m_Instances.ContainsKey(key))
            {
                m_Instances[key] = instance;
            }
            else
            {
                m_Instances.Add(key, instance);
            }
        }

        public T Get<T>()where T:class
        {
            var key = typeof(T);
            if (m_Instances.TryGetValue(key,out var  retinstance))
            {
                return retinstance as T;
            }
            return null ;
        }
    }

}
