using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;

public class ObjectPool <T> : MonoBehaviour where T : MonoBehaviour
{
    private T prefab;
    private int poolCount;

    private Queue<T> pool = new Queue<T>();

    public ObjectPool(T prefab , int poolCount)
    {
        this.prefab = prefab;
        this.poolCount = poolCount;
        InitPool();
    }


    private void InitPool()
    {
        if (prefab == null) return;

        for(int i = 0; i < poolCount; i++)
        {
            T go = Instantiate(prefab, transform);
            
            go.gameObject.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public void EnqueueObject(T go)
    {
        pool.Enqueue(go);
        go.transform.SetParent(transform);
        go.gameObject.SetActive(false);
    }

    public T GetObject()
    {
        T obj;
        if (pool.Count == 0)
        {
            var create = Instantiate(prefab);
            EnqueueObject(create);
        }

        obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

}
