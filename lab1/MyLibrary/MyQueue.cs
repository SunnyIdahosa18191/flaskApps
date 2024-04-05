using System.Collections;

namespace MyLibrary
{
    public class MyQueue<T> : IEnumerable<T>
    {
        public delegate void EventHandler(string message);
        public event EventHandler? Notify;

        private T[] _array;
        private int _head;
        private int _tail;
        private int _size;
        private static T[] _emptyArray = Array.Empty<T>();

        public MyQueue(int capacity)
        {
            if (capacity < 0)
                throw new IndexOutOfRangeException();
            this._array = new T[capacity];
            this._head = 0;
            this._tail = 0;
            this._size = 0;
        }
        public MyQueue() => this._array = MyQueue<T>._emptyArray;
        public int Size() => this._size;

        public bool Contains(T item)
        {
            int index = this._head;
            int size = this._size;
            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
            while (size-- > 0)
            {
                if (equalityComparer.Equals(this._array[index], item))
                    return true;
                index = (index + 1) % this._array.Length;
            }
            return false;
        }
        public void Enqueue(T item)
        {
            if (this._size == this._array.Length)
            {
                int capacity = this._array.Length + 1;
                this.SetCapacity(capacity);
            }
            this._array[this._tail] = item;
            this._tail = (this._tail + 1) % this._array.Length;
            Notify?.Invoke($"Element \"{item}\" is enqueued.");
            ++this._size;
        }
        private void SetCapacity(int capacity)
        {
            T[] destinationArray = new T[capacity];
            if (this._size > 0)
            {
                if (this._head < this._tail)
                {
                    Array.Copy((Array)this._array, this._head, (Array)destinationArray, 0, this._size);
                }
                else
                {
                    Array.Copy((Array)this._array, this._head, (Array)destinationArray, 0, this._array.Length - this._head);
                    Array.Copy((Array)this._array, 0, (Array)destinationArray, this._array.Length - this._head, this._tail);
                }
            }
            this._array = destinationArray;
            this._head = 0;
            this._tail = this._size == capacity ? 0 : this._size;
        }

        public T Dequeue()
        {
            if (this._size == 0)
                throw new InvalidOperationException();
            T obj = this._array[this._head];
            this._array[this._head] = default(T);
            this._head = (this._head + 1) % this._array.Length;     
            --this._size;
            Notify?.Invoke($"Element \"{obj}\" is dequeued.");
            return obj;
        }
        public T Peek()
        {
            if (this._size == 0)
                throw new InvalidOperationException();
            return this._array[this._head];
        }
        public void Clear()
        {
            Array.Clear((Array)this._array);
            this._head = 0;
            this._tail = 0;
            this._size = 0;
            Notify?.Invoke($"The collection is cleared");
        }

        public MyQueue<T>.Enumerator GetEnumerator() => new(this);
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>)new MyQueue<T>.Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => new MyQueue<T>.Enumerator(this);
        internal T GetElement(int i) => this._array[(this._head + i) % this._array.Length];

        public class Enumerator : IEnumerator
        {
            private MyQueue<T> _q;
            private int _index;
            private T _currentElement;

            internal Enumerator(MyQueue<T> q)
            {
                this._q = q;
                this._index = -1;
                this._currentElement = default(T);
            }

            public bool MoveNext()
            {
                if (this._index == -2)
                    return false;
                ++this._index;
                if (this._index == this._q._size)
                {
                    this._index = -2;
                    this._currentElement = default(T);
                    return false;
                }
                this._currentElement = this._q.GetElement(this._index);
                return true;
            }

            public T Current
            {
                get
                {
                    if (this._index < 0)
                        throw new InvalidOperationException();
                    return this._currentElement;
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    if (this._index < 0)
                        throw new InvalidOperationException();
                    return (object)this._currentElement;
                }
            }
            void IEnumerator.Reset()
            {
                this._index = -1;
                this._currentElement = default(T);
            }
        }
    }
}
