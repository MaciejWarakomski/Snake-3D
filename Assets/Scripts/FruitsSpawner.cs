using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsSpawner : MonoBehaviour
{
    [SerializeField] int maxXPos = 11;
    [SerializeField] int maxYPos = 8;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 2f;
    RaycastHit hitInfo;

    [SerializeField] List<GameObject> fruits;

    bool isWaitingForSpawn;


    private void Awake()
    {
        isWaitingForSpawn = false;
        StartCoroutine(StartSpawningFruit());
    }

    private void Update()
    {
        if (isWaitingForSpawn)
        {
            SpawnFruit();
        }
    }

    public IEnumerator StartSpawningFruit()
    {
        yield return new WaitForSeconds
            (Random.Range(minSpawnDelay, maxSpawnDelay));
        SpawnFruit();
    }

    private void SpawnFruit()
    {
        float fruitPosX;
        float fruitPosY;

        fruitPosX = Random.Range(-maxXPos, maxXPos + 1);
        fruitPosY = Random.Range(-maxYPos, maxYPos + 1);

        Physics.Raycast(
            new Vector3(fruitPosX, 5f, fruitPosY),
            Vector3.down,
            out hitInfo);

        if (hitInfo.collider.transform.tag.Equals("Background"))
        {
            Instantiate(
            fruits[Random.Range(0, fruits.Count)],
            new Vector3(fruitPosX, 0f, fruitPosY),
            Quaternion.identity);

            isWaitingForSpawn = false;
        }
        else
        {
            isWaitingForSpawn = true;
        }
    }
}