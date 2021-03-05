using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveTimePeriod = 1f;
    [SerializeField] GameObject snakeBody;
    [SerializeField] GameObject snakeTail;
    List<GameObject> snakeBodyList = new List<GameObject>();
    Vector3 lastSnakePosition;
    Quaternion lastSnakeRotation;
    float moveTimer = 0f;
    int snakeSize;

    FruitSpawner fruitSpawnerScript;

    private void Awake()
    {
        fruitSpawnerScript = FindObjectOfType<FruitSpawner>();
        lastSnakePosition = transform.position;
    }

    void Update()
    {
        ProcessDirection();
        ProcessMove();
    }

    private void ProcessDirection()
    {
        Vector3 positionsDiff = lastSnakePosition - transform.position;
        var distance = positionsDiff.magnitude;
        var direction = positionsDiff / distance;

        if (Input.GetKeyDown(KeyCode.UpArrow) && (direction.z != 1 || snakeBodyList.Count == 0))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && (direction.x != 1 || snakeBodyList.Count == 0))
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && (direction.z != -1 || snakeBodyList.Count == 0))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && (direction.x != -1 || snakeBodyList.Count == 0))
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

            lastSnakePosition = transform.position;
            lastSnakeRotation = transform.rotation;

            transform.Translate(Vector3.forward);

            var bodyClone = Instantiate(snakeBody, lastSnakePosition, lastSnakeRotation);
            snakeBodyList.Insert(0, bodyClone);

            while (snakeBodyList.Count >= snakeSize + 1)
            {
                Destroy(snakeBodyList[snakeBodyList.Count - 1]);
                snakeBodyList.RemoveAt(snakeBodyList.Count - 1);
            }
            if (snakeBodyList.Count > 0)
            {
                GameObject lastBodySnake = snakeBodyList[snakeBodyList.Count - 1];
                Vector3 lastBodyPosition = lastBodySnake.transform.position;
                Quaternion lastBodyRotation = lastBodySnake.transform.rotation;
                Destroy(snakeBodyList[snakeBodyList.Count - 1]);
                snakeBodyList.RemoveAt(snakeBodyList.Count - 1);
                var tailClone = Instantiate(snakeTail, lastBodyPosition, lastBodyRotation);
                snakeBodyList.Insert(snakeBodyList.Count, tailClone);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fruit":
                Destroy(other.gameObject);
                snakeSize++;
                StartCoroutine(fruitSpawnerScript.StartSpawningFruit());
                break;
            case "Spikes":
                Destroy(other.gameObject);
                if (snakeSize > 0)
                {
                    snakeSize--;
                    FindObjectOfType<SpikeSpawner>().RemoveSpikes();
                }
                else
                {
                    FindObjectOfType<SceneHolder>().ReloadScene();
                }
                break;
            case "Fence":
                FindObjectOfType<SceneHolder>().ReloadScene();
                break;
            case "Body":
                FindObjectOfType<SceneHolder>().ReloadScene();
                break;
            default:
                break;
        }
    }
}