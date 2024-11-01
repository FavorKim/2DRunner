using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : Singleton<PlatformManager>
{
    [SerializeField] private Platform platformPrefab;
    [SerializeField] private int poolCount;
    private ObjectPool<Platform> platformPool;


    [SerializeField, Range(1f, 5.0f)] private float spawnSpeed;
    public float SpawnSpeed 
    {
        get
        {
            return spawnSpeed; 
        }
        set
        {
            spawnSpeed = value;
            OnGameSpeedChange_SetSpawnCooltime();
        }
    }
    [SerializeField] private float spawnCool;

    public bool isStop = false;

    private List<float> spawnPosList = new List<float>()
    {
        -4,
        -2.5f,
        -1.0f,
        0.5f,
        2.0f,
    };

    private int prevSpawnPos = 0;

    protected override void Start()
    {
        base.Start();
        platformPool = new ObjectPool<Platform>(platformPrefab, poolCount, transform);
        OnGameSpeedChange_SetSpawnCooltime();
        EventManager.Instance.AddListener("OnGameSpeedChange", OnGameSpeedChange_SetSpawnCooltime, out bool already);
        StartCoroutine(CorSpawnPlatform());
    }


    private void SpawnPlatform()
    {
        int maxRange = prevSpawnPos + 2;

        if (maxRange >= 5) maxRange = 5;

        int rand = Random.Range(0, maxRange);
        prevSpawnPos = rand;
        float spawnYPos = spawnPosList[rand];
        Vector3 spawnPos = new Vector3(GameManager.Instance.GetRightEdge(), spawnYPos);
        Platform platform = platformPool.GetObject();

        platform.transform.position = spawnPos;

        platform.InvokeOnSpawnPlatform();
    }

    private IEnumerator CorSpawnPlatform()
    {
        float t = 0;
        while (true)
        {
            if (!isStop)
            {
                if (t > spawnCool)
                {
                    SpawnPlatform();
                    t = 0;
                }
            }
            else
            {
                t = 0;
                yield return null;
            }
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void ReturnPlatform(Platform platform)
    {
        platformPool.EnqueueObject(platform);
    }

    private void OnGameSpeedChange_SetSpawnCooltime()
    {
        spawnCool = spawnSpeed / GameManager.Instance.GameSpeed;
    }
}

