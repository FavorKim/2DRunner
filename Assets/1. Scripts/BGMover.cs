using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BackGroundLeft;
    [SerializeField] private SpriteRenderer BackGroundRight;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        
    }
    private float GetLength()
    {
        Bounds bound = BackGroundLeft.bounds;
        float leftMax = bound.min.x;
        float rightMax = bound.max.x;
        float length = rightMax - leftMax;

        return length;
    }

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
        BackGroundLeft.transform.Translate(Vector2.left * Time.deltaTime * GameManager.Instance.GameSpeed * moveSpeed * 0.1f);
        BackGroundRight.transform.Translate(Vector2.left * Time.deltaTime * GameManager.Instance.GameSpeed * moveSpeed * 0.1f);
    }

    private GameObject IsLeftEnd()
    {

        if (BackGroundLeft.transform.localPosition.x <= GetLength() * - 1)
        {
            return BackGroundLeft.gameObject;
        }
        else if(BackGroundRight.transform.localPosition.x <= GetLength() * - 1)
        {
            return BackGroundRight.gameObject;
        }
        return null;
    }

    private void RelayBackGround(GameObject isLeftEnd)
    {
        if (isLeftEnd != null)
        {
            Vector3 newPos = isLeftEnd.transform.localPosition;
            newPos.x += GetLength() * 2;
            isLeftEnd.transform.localPosition = newPos;
        }
    }
}
