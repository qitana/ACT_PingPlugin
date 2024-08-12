using System;
using System.Collections.Generic;


namespace Qitana.PingPlugin
{
    public class FixedSizeQueue<T>
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();
        private readonly int _maxSize;
        private readonly object _lock = new object();

        public FixedSizeQueue(int maxSize)
        {
            _maxSize = maxSize;
        }

        public void Enqueue(T item)
        {
            lock (_lock)
            {
                if (_list.Count == _maxSize)
                {
                    _list.RemoveFirst();
                }
                _list.AddLast(item);
            }
        }

        public T Dequeue()
        {
            lock (_lock)
            {
                if (_list.Count == 0)
                {
                    throw new InvalidOperationException("Queue is empty.");
                }

                var value = _list.First.Value;
                _list.RemoveFirst();
                return value;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _list.Clear();
            }
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _list.Count;
                }
            }
        }

        public List<T> ToArray()
        {
            lock (_lock)
            {
                return new List<T>(_list);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return new List<T>(_list).GetEnumerator();
            }
        }
    }
}
