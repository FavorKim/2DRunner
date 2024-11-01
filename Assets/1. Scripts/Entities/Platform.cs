using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    private float platformMoveSpeed;
    [SerializeField] private float platformSpeedVal;
    [SerializeField] private Transform smallObsMinPos;
    [SerializeField] private Transform smallObsMaxPos;
    [SerializeField] private Transform bigObsPos;

    public UnityAction OnDisablePlatform;
    UnityAction OnSpawnPlatform;

    private void Start()
    {
        OnGameSpeedChange_SetPlatformMoveSpeed();
        EventManager.Instance.AddListener("OnGameSpeedChange", OnGameSpeedChange_SetPlatformMoveSpeed, out bool already);

        
    }
    private void OnEnable()
    {
        OnDisablePlatform += OnDisablePlatform_EnqueueObject;
        OnSpawnPlatform += OnSpawnPlatform_SetObstacle;
    }
    private void OnDisable()
    {
        OnSpawnPlatform -= OnSpawnPlatform_SetObstacle;
        OnDisablePlatform -= OnDisablePlatform_EnqueueObject;
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * platformMoveSpeed);
        if(transform.position.x < GameManager.Instance.GetLeftEdge()*1.5f)
        {
            if (OnDisablePlatform == null)
                OnDisablePlatform += OnDisablePlatform_EnqueueObject;
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
            bigObs.transform.SetParent(bigObsPos);
            bigObs.transform.localPosition = Vector3.zero;
            OnDisablePlatform += bigObs.OnPlatformDisable_EnqueueObstacle;
        }
        else
        {
            Obstacle smallObs = ObstacleManager.Instance.GetSmallObs();
            float xPos = Random.Range(smallObsMinPos.localPosition.x, smallObsMaxPos.localPosition.x);
            smallObs.transform.SetParent(smallObsMaxPos.parent);
            smallObs.transform.localPosition = new Vector2(xPos, 0);
            OnDisablePlatform += smallObs.OnPlatformDisable_EnqueueObstacle;
        }
    }

    public void InvokeOnSpawnPlatform()
    {
        
        OnSpawnPlatform?.Invoke();
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
