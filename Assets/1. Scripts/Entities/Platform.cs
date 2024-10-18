using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private float platformMoveSpeed;
    [SerializeField] private float platformSpeedVal;



    private void Start()
    {
        OnGameSpeedChange_SetPlatformMoveSpeed();
        EventManager.Instance.AddListener("OnGameSpeedChange", OnGameSpeedChange_SetPlatformMoveSpeed, out bool already);
    }
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * platformMoveSpeed);
        if(transform.position.x < -15.0f)
        {
            PlatformManager.Instance.EnqueueObject(this.gameObject);
        }
    }

    private void OnGameSpeedChange_SetPlatformMoveSpeed()
    {
        platformMoveSpeed = GameManager.Instance.GameSpeed * platformSpeedVal;
    }
}
