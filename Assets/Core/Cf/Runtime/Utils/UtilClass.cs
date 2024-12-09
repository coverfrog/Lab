using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Cf.Utils
{
    public static partial class Util
    {
        public static class Class
        {
            #region < FindSubClass >

            public static Type[] FindSubClassTypes(Type baseType)
            {
                if (!baseType.IsClass)
                {
                    return null;
                }

                Assembly assembly = Assembly.GetAssembly(baseType);

                if (assembly == null)
                {
                    return Array.Empty<Type>();
                }

                Type[] allTypes = assembly.GetTypes();
                Type[] subTypes = allTypes
                    .Where(type => type.IsClass && !type.IsAbstract && baseType.IsAssignableFrom(type) && type != baseType)
                    .ToArray();

                return subTypes;
            }

            public static Type[] FindSubClassTypes<T>() where T : class
            {
                Type baseType = typeof(T);

                return FindSubClassTypes(baseType);
            }

            public static string[] FindSubClassNames(Type baseType)
            {
                return FindSubClassTypes(baseType)
                    .Select(t => t.Name)
                    .ToArray();
            }


            public static string[] FindSubClassNames<T>() where T : class
            {
                Type baseType = typeof(T);

                return FindSubClassNames(baseType);
            }
            
            public static string[] FindSubClassFullNames(Type baseType)
            {
                return FindSubClassTypes(baseType)
                    .Select(t => t.Name)
                    .ToArray();
            }

            public static string[] FindSubClassFullNames<T>() where T : class
            {
                Type baseType = typeof(T);

                return FindSubClassFullNames(baseType);
            }

            #endregion
        }
    }
}
