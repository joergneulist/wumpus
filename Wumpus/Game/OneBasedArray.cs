using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Wumpus.Game
{
    class OneBasedArray<T> : IEnumerable<T>
    {
        private T[] data;

        public OneBasedArray(int size)
        {
            data = new T[size];
        }

        public int Length { get => data.Length; private set { } }

        public T this[int key] { get => data[key - 1]; set => data[key - 1] = value; }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)data).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)data).GetEnumerator();
    }
}
