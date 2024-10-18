using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolCount;

    private Queue<GameObject> pool = new Queue<GameObject>();

    protected override void Start()
    {
        base.Start();
        InitPool();
    }

    private void InitPool()
    {
        if (prefab == null) return;

        GameObject parent = new GameObject(prefab.name + "Pool");
        parent.transform.SetParent(transform);
        for(int i = 0; i < poolCount; i++)
        {
            GameObject go = Instantiate(prefab, parent.transform.transform);
            
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public void EnqueueObject(GameObject go)
    {
        pool.Enqueue(go);
        go.SetActive(false);
    }

    public GameObject GetObject()
    {
        GameObject obj;
        if (pool.Count == 0)
        {
            var create = Instantiate(prefab);
            EnqueueObject(create);
        }

        obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }
}
