using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField,Range(1,5)] private float gameSpeed;

    [SerializeField] private BGMover bgmover;
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

    public float GetRightEdge()
    {
        Vector3 edge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        float rightEdgeXPos = edge.x;
        return rightEdgeXPos;
    }
    public float GetLeftEdge()
    {
        Vector3 edge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        float leftEdgeXPos = edge.x;
        return leftEdgeXPos;
    }
}
