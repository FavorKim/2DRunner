using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    [SerializeField] private Obstacle smallObsPrefab;
    [SerializeField] private Obstacle bigObsPrefab;
    [SerializeField] private int poolCount;

    private ObjectPool<Obstacle> smallObsPool;
    private ObjectPool<Obstacle> bigObsPool;

    protected override void Start()
    {
        smallObsPool = new ObjectPool<Obstacle>(smallObsPrefab, poolCount, transform);
        bigObsPool = new ObjectPool<Obstacle>(bigObsPrefab, poolCount, transform);
        base.Start();
    }

    public Obstacle GetSmallObs()
    {
        Obstacle smallobs = smallObsPool.GetObject();
        return smallobs;
    }
    public Obstacle GetBigObs()
    {
        Obstacle bigObs = bigObsPool.GetObject();
        return bigObs;
    }

    public void ReturnSmallObs(Obstacle obj)
    {
        smallObsPool.EnqueueObject(obj);
    }

    public void ReturnBigObs(Obstacle obj)
    {
        bigObsPool.EnqueueObject(obj);
    }

}
