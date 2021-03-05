using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveTimePeriod = 1f;
    [SerializeField] Vector2Int startingPosition = new Vector2Int(0, 0);
    Vector2Int snakePos;
    Quaternion snakeRotation;
    float moveTimer = 0f;

    private void Awake()
    {
        snakePos = startingPosition;
        transform.position = new Vector3(snakePos.x, 0, snakePos.y);
    }

    void Update()
    {
        ProcessDirection();
        ProcessMove();

    }

    private void ProcessDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
    }

    private void ProcessMove()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveTimePeriod)
        {
            transform.Translate(Vector3.forward);
            moveTimer = 0f;
        }
    }
}
