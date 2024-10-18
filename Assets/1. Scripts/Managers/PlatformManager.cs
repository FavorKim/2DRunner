using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : ObjectPoolManager
{

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

    private List<Vector2> spawnPosList = new List<Vector2>()
    {
        new Vector2(24, -4),
        new Vector2(24, -2.5f),
        new Vector2(24, -1.0f),
        new Vector2(24, 0.5f),
        new Vector2(24, 2.0f),
    };

    private int prevSpawnPos = 0;

    protected override void Start()
    {
        base.Start();
        OnGameSpeedChange_SetSpawnCooltime();
        StartCoroutine(CorSpawnPlatform());
        EventManager.Instance.AddListener("OnGameSpeedChange", OnGameSpeedChange_SetSpawnCooltime, out bool already);
    }


    private void SpawnPlatform()
    {
        int maxRange = prevSpawnPos + 2;

        if (maxRange >= 5) maxRange = 5;

        int rand = Random.Range(0, maxRange);
        prevSpawnPos = rand;
        Vector2 spawnPos = spawnPosList[rand];

        GameObject platform = GetObject();

        platform.transform.position = spawnPos;
    }

    private IEnumerator CorSpawnPlatform()
    {
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            yield return null;
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

        }
    }

    private void OnGameSpeedChange_SetSpawnCooltime()
    {
        spawnCool = spawnSpeed / GameManager.Instance.GameSpeed;
    }
}

