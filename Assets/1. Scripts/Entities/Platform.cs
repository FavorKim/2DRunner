using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    private float platformMoveSpeed;
    [SerializeField] private float platformSpeedVal;
    [SerializeField] private Transform smallObsMinPos;
    [SerializeField] private Transform smallObsMaxPos;
    [SerializeField] private Transform bigObsPos;

    UnityAction OnDisablePlatform;
    UnityAction OnSpawnPlatform;

    private void Start()
    {
        OnGameSpeedChange_SetPlatformMoveSpeed();
        EventManager.Instance.AddListener("OnGameSpeedChange", OnGameSpeedChange_SetPlatformMoveSpeed, out bool already);

        OnDisablePlatform += OnDisablePlatform_EnqueueObject;
        OnSpawnPlatform += OnSpawnPlatform_SetObstacle;
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * platformMoveSpeed);
        if(transform.position.x < -15.0f)
        {
            OnDisablePlatform.Invoke();
            OnDisablePlatform = null;
        }
    }

    private void OnSpawnPlatform_SetObstacle()
    {
        int rand = Random.Range(0, 3);
        if(rand == 0)
        {
            Obstacle bigObs = ObstacleManager.Instance.GetBigObs();
            bigObs.transform.SetParent(transform);
            bigObs.transform.localPosition = bigObsPos.position;

            OnDisablePlatform += bigObs.OnPlatformDisable_EnqueueObstacle;
        }
        else
        {
            Obstacle smallObs = ObstacleManager.Instance.GetSmallObs();
            float xPos = Random.Range(smallObsMinPos.position.x, smallObsMaxPos.position.x);
            smallObs.transform.SetParent(transform);
            smallObs.transform.localPosition = new Vector2(xPos, smallObsMinPos.position.y);

            OnDisablePlatform += smallObs.OnPlatformDisable_EnqueueObstacle;
        }
    }

    public void InvokeOnSpawnPlatform()
    {
        OnSpawnPlatform.Invoke();
    }


    private void OnDisablePlatform_EnqueueObject()
    {
        PlatformManager.Instance.ReturnPlatform(this);
    }

    private void OnGameSpeedChange_SetPlatformMoveSpeed()
    {
        platformMoveSpeed = GameManager.Instance.GameSpeed * platformSpeedVal;
    }
}
