using NPLH;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Queue<IPoolable> _poolables;

    public void InitPool(int size)
    {
        _poolables = new Queue<IPoolable>(size);
    }

    public void Deactivate(IPoolable obj)
    {
        _poolables.Enqueue(obj);
        obj.OnDeactivate();
    }

    public void Activate(object argument = default) 
    {
        if (_poolables.Count > 0) 
        {
            _poolables.Dequeue().OnActivate(argument);
        }
    }
}
