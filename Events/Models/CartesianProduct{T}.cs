using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Models
{
    class CartesianProduct<T> : IEnumerable<T[]>
    {
        public CartesianProduct(IEnumerable<IEnumerable<T>> sets)
        {
            Sets = sets
                .Select(s => s.ToArray())
                .ToArray();
        }

        T[][] Sets { get; }

        IEnumerable<T[]> Iterate(IEnumerable<int> idecies)
        {
            var level = idecies.Count();
            if (level == Sets.Length)
                yield return idecies
                    .Select((idx, pos) => Sets[pos][idx])
                    .ToArray();
            else
                for (int i = 0; i < Sets[level].Length; i++)
                    foreach (var combination in Iterate(idecies.Concat(new[] { i })))
                        yield return combination;
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return Iterate(Enumerable.Empty<int>())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
