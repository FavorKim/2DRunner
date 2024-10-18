using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField,Range(1,5)] private float gameSpeed;
    public float GameSpeed
    {
        get { return gameSpeed; }
        set
        {
            if(gameSpeed != value)
            {
                gameSpeed = value;
                EventManager.Instance.InvokeEventHanler("OnGameSpeedChange");
            }
        }
    }

    protected override void Start()
    {
        base.Start();
    }
}
