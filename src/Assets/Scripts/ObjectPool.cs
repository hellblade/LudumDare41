using System;
using System.Collections.Generic;

public class ObjectPool<T>
{

    Stack<T> pool = new Stack<T>();

    public bool TryGet(ref T result)
    {
        if (pool.Count > 0)
        {
            result = pool.Pop();
            return true;
        }

        return false;
    }

    public void Free(T item)
    {
        pool.Push(item);
    }

}
