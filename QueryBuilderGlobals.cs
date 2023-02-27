using System;
using System.Linq;
using Scellecs.Morpeh.Collections;
using UnityEngine;

namespace Scellecs.Morpeh
{
    public class QueryBuilderGlobals
    {
        internal static readonly FastList<Type> TYPES_TO_REQUIRE = new();
        internal static readonly FastList<Type> TYPES_TO_IGNORE = new();
        
#if UNITY_2019_1_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            TYPES_TO_REQUIRE.Clear();
            TYPES_TO_IGNORE.Clear();
        }
#endif
        
        public static void With<T>() where T: struct, IComponent
        {
            var type = typeof(T);
            if (TYPES_TO_REQUIRE.Contains(type))
                return;
            TYPES_TO_REQUIRE.Add(type);
        }
        
        public static void Without<T>() where T: struct, IComponent
        {
            var type = typeof(T);
            if (TYPES_TO_IGNORE.Contains(type))
                return;
            TYPES_TO_IGNORE.Add(type);
        }
    }
}