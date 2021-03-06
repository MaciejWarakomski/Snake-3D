using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControl : MonoBehaviour
{
    Vector3 lastSnakePosition;
    Quaternion lastSnakeRotation;
    float moveTimePeriod;
    float moveTimer;
    int snakeSize;

    [SerializeField] GameObject snakeBody;
    [SerializeField] GameObject snakeTail;
    [SerializeField] AudioClip fruitSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip deathSound;

    List<GameObject> snakeBodyList = new List<GameObject>();
    FruitsSpawner fruitSpawnerScript;
    AudioSource audioSource;

    bool isAlive;

    private void Awake()
    {
        fruitSpawnerScript = FindObjectOfType<FruitsSpawner>();
        lastSnakePosition = transform.position;
        moveTimer = 0f;
        GetDifficulty();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetEffectsVolume();
        isAlive = true;
    }

    private void GetDifficulty()
    {
        switch (PlayerPrefsController.GetDifficulty())
        {
            case 0:
                moveTimePeriod = 0.5f;
                break;
            case 1:
                moveTimePeriod = 0.3f;
                break;
            case 2:
                moveTimePeriod = 0.1f;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            ProcessDirection();
            ProcessMove();
        }
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
            ProcessTail();
        }
    }

    private void ProcessTail()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fruit":
                AudioSource.PlayClipAtPoint(fruitSound, Camera.main.transform.position);
                Destroy(other.gameObject);
                snakeSize++;
                StartCoroutine(fruitSpawnerScript.StartSpawningFruit());
                break;
            case "Spikes":
                Destroy(other.gameObject);
                DecreaseSnakeSize();
                break;
            case "Fence":
                ProcessDeath();
                break;
            case "Body":
                ProcessDeath();
                break;
            default:
                break;
        }
    }

    private void DecreaseSnakeSize()
    {
        if (snakeSize > 0)
        {
            AudioSource.PlayClipAtPoint(crashSound, Camera.main.transform.position);
            snakeSize--;
            FindObjectOfType<SpikesSpawner>().RemoveSpikes();
        }
        else
        {
            ProcessDeath();
        }
    }

    private void ProcessDeath()
    {
        isAlive = false;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        FindObjectOfType<GameManager>().SetScore(snakeBodyList.Count);
        FindObjectOfType<SceneLoader>().LoadGameOver();
    }
}