using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSizeControl : MonoBehaviour
{
    [SerializeField] GameObject snakeBody;
    [SerializeField] GameObject snakeTail;
    Movement movementScript;
    int snakeSize;

    private void Awake()
    {
        movementScript = GetComponent<Movement>();
        snakeSize = 0;
    }

    public void SpawnSnakeBody()
    {
        if(snakeSize > 1)
        {
            var snakeBodyClone = Instantiate(snakeBody, movementScript.GetSnakePosition(), transform.rotation);
            snakeBodyClone.transform.parent = gameObject.transform;
        }
        else if(snakeSize == 1)
        {
            var snakeTailClone = Instantiate(snakeTail, movementScript.GetSnakePosition(), transform.rotation);
            snakeTailClone.transform.parent = gameObject.transform;
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
