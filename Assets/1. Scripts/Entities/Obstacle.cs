using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void Start()
    {
    }


    public void OnPlatformDisable_EnqueueObstacle()
    {
        if (gameObject.name == "bigObs")
            ObstacleManager.Instance.ReturnBigObs(this);
        else if (gameObject.name == "smallObs")
            ObstacleManager.Instance.ReturnSmallObs(this);
    }


}
