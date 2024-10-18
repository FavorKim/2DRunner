using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    [SerializeField] private GameObject BackGroundLeft;
    [SerializeField] private GameObject BackGroundRight;
    [SerializeField] private float moveSpeed;

    private Vector2 pos = new Vector2(24, 0);

    private void Update()
    {
        MoveBackGroundRelay();
    }

    private void MoveBackGroundRelay()
    {
        MoveBackGrounds();
        RelayBackGround(IsLeftEnd());
    }

    private void MoveBackGrounds()
    {
        BackGroundLeft.transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        BackGroundRight.transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
    }

    private GameObject IsLeftEnd()
    {

        if (BackGroundLeft.transform.localPosition.x <= -24)
        {
            return BackGroundLeft;
        }
        else if(BackGroundRight.transform.localPosition.x <= -24)
        {
            return BackGroundRight;
        }
        return null;
    }

    private void RelayBackGround(GameObject isLeftEnd)
    {
        if (isLeftEnd != null)
        {
            isLeftEnd.transform.localPosition = pos;
        }
    }
}
