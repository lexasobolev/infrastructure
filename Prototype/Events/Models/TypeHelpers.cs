using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Events.Models
{
    static class TypeHelpers
    {
        public static IEnumerable<Type> GetCovariantTypes(this Type type)
        {
            var args = type.GetGenericArguments();
            var covariantIndices = type
                .GetCovariantArgumentIndices()
                .ToArray();

            if (!covariantIndices.Any())
            {
                yield return type;
                yield break;
            }

            foreach (var permutation in new CartesianProduct<Type>(from ca in covariantIndices.Select(i => args[i])
                                                                   select ca.GetImplementedTypes()))
            {
                for (int i = 0; i < permutation.Length; i++)
                    args[covariantIndices[i]] = permutation[i];

                yield return type.GetGenericTypeDefinition().MakeGenericType(args.ToArray());
            }
        }

        public static Type GetHandlerType(this Type type)
        {
            return typeof(IHandler<>).MakeGenericType(type);
        }

        public static Type GetEnumerableType(this Type type)
        {
            return typeof(IEnumerable<>).MakeGenericType(type);
        }

        static IEnumerable<int> GetCovariantArgumentIndices(this Type type)
        {
            var result = new HashSet<int>();
            var args = type.GetGenericArguments();
            for (int i = 0; i < args.Length; i++)
                foreach (var itf in type.GetInterfaces())
                {
                    var itfArgs = itf.GetGenericArguments();
                    for (int j = 0; j < itfArgs.Length; j++)
                        if (args[i] == itfArgs[j])
                            if ((itf
                                .GetGenericTypeDefinition()
                                .GetGenericArguments()[j]
                                .GenericParameterAttributes & GenericParameterAttributes.Covariant) != 0)
                                result.Add(i);
                }

            return result;
        }

        static IEnumerable<Type> GetImplementedTypes(this Type type)
        {
            var types = new List<Type>(type.GetInterfaces());
            while (type != null)
            {
                types.Add(type);
                type = type.BaseType;
            }

            return types;
        }
    }
}
