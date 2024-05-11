using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCToolsKidForJava.Utils
{
    internal class MapUtil
    {
        public static V get<K, V>(Dictionary<K,V> map,K key)
        {
            if (map.ContainsKey(key))
            {
                return map[key];
            }
            return default;
        }
    }
}
