using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveTimePeriod = 1f;
    [SerializeField] Vector3 startingPosition = new Vector3(0, 0, 0);
    [SerializeField] GameObject snakeBody;
    [SerializeField] GameObject snakeTail;
    List<Vector3> snakePositionsList = new List<Vector3>();
    Vector3 snakePosition;
    float moveTimer = 0f;
    int snakeSize;

    private void Awake()
    {
        snakePosition = startingPosition;
        transform.position = new Vector3(snakePosition.x, 0, snakePosition.y);
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
            if (snakePositionsList.Count > 0)
            {
                if ((transform.position - snakePositionsList[0]).z >= 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (snakePositionsList.Count > 0)
            {
                if ((transform.position - snakePositionsList[0]).x >= 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (snakePositionsList.Count > 0)
            {
                if ((transform.position - snakePositionsList[0]).z <= 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (snakePositionsList.Count > 0)
            {
                if ((transform.position - snakePositionsList[0]).x <= 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            }
        }
    }

    private void ProcessMove()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveTimePeriod)
        {
            moveTimer = 0f;
            snakePosition = transform.position;

            snakePositionsList.Insert(0, snakePosition);

            transform.Translate(Vector3.forward);

            while (snakePositionsList.Count >= snakeSize + 1)
            {
                snakePositionsList.RemoveAt(snakePositionsList.Count - 1);
            }

            for (int i = 0; i < snakePositionsList.Count; i++)
            {
                Vector3 bodyPosition = snakePositionsList[i];
                var bodyClone = Instantiate(snakeBody, bodyPosition, Quaternion.identity);
                Destroy(bodyClone, moveTimePeriod);
            }
        }
    }

    public void AddSnakeSize()
    {
        snakeSize++;
    }

    public void RemoveSnakeSize()
    {
        if (snakeSize > 0)
        {
            snakeSize--;
        }
        else
        {
            FindObjectOfType<SceneHolder>().ReloadScene();
        }
    }
}