using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveTimePeriod = 1f;
    [SerializeField] Vector3 startingPosition = new Vector3(0, 0, 0);
    [SerializeField] GameObject snakeBody;
    Vector3 snakePosition;
    float moveTimer = 0f;

    SnakeSizeControl snakeSizeControlScript;

    private void Awake()
    {
        snakePosition = startingPosition;
        transform.position = new Vector3(snakePosition.x, 0, snakePosition.y);
        snakeSizeControlScript = GetComponent<SnakeSizeControl>();
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
            moveTimer = 0f;
            snakePosition = transform.position;
            transform.Translate(Vector3.forward);
            snakeSizeControlScript.SpawnSnakeBody();
        }
    }

    public Vector3 GetSnakePosition()
    {
        return snakePosition;
    }
}