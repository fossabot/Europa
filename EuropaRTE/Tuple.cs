using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuropaRTL
{
    namespace Types
    {
        /// <summary>
        /// This is a tuple that if initialized empty is left writeable, however once written to locks itself
        /// </summary>
        /// <exception cref="TypeAccessException"></exception>
        public class Tuple<T> : IEnumerable<T>
        {
            private List<T> data;
            private bool @lock;
            Tuple()
            {
                @lock = false;
                data = new List<T>();
            }

            Tuple(T[] i)
            {
                @lock = true;
                data = new List<T>(i);
            }

            [System.Runtime.CompilerServices.IndexerName("EUTupleIndex")]
            public T this[int index]
            {
                get => data[index];
                set
                {
                    data[index] = @lock ? throw new TypeAccessException("Tried to write to a locked tuple") : value;
                    @lock = true;
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                return data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
