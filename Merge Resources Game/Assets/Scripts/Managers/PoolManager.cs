using NPLH;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public Pool AddPool(string type, int size = 0)
    {

        GameObject poolsGO = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");

        GameObject poolGO = new GameObject($"Pool: {type}");
        poolGO.transform.SetParent(poolsGO.transform);
        var pool = poolGO.AddComponent<Pool>();
        pool.InitPool(size);
        _pools.Add(type, pool);


        return pool;
    }
    public Pool GetPool(string type)
    {
        Pool pool;
        if (_pools.TryGetValue(type, out pool) == false)
        {
            AddPool(type);
            _pools.TryGetValue(type, out pool);
        }

        return pool;
    }
}
