using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;

public class ObjectPool <T> where T : MonoBehaviour
{
    private T prefab;
    private int poolCount;
    private Transform transform;

    private Queue<T> pool = new Queue<T>();

    public ObjectPool(T prefab , int poolCount, Transform transform)
    {
        this.prefab = prefab;
        this.poolCount = poolCount;
        InitPool();
        GameObject tra = new GameObject($"{transform.name}Pool");
        tra.transform.parent = transform;
        this.transform = tra.transform;
    }


    private void InitPool()
    {
        if (prefab == null) return;

        for(int i = 0; i < poolCount; i++)
        {
            T go = GameObject.Instantiate(prefab, transform);
            
            go.gameObject.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public void EnqueueObject(T go)
    {
        pool.Enqueue(go);
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.gameObject.SetActive(false);
    }

    public T GetObject()
    {
        T obj;
        if (pool.Count == 0)
        {
            var create = GameObject.Instantiate(prefab);
            EnqueueObject(create);
        }

        obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

}
