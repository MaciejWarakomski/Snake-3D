using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] int maxXPos = 11;
    [SerializeField] int maxYPos = 8;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 2f;
    [SerializeField] List<GameObject> fruits;
    RaycastHit hitInfo;

    void Start()
    {
        StartCoroutine(StartSpawningFruit());
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
        do
        {
            fruitPosX = Random.Range(-maxXPos, maxXPos + 1);
            fruitPosY = Random.Range(-maxYPos, maxYPos + 1);

            Physics.Raycast(
                new Vector3(fruitPosX, 2f, fruitPosY),
                Vector3.down,
                out hitInfo);

        } while (hitInfo.collider.transform.tag.Equals("Background"));

        if(fruits.Count == 0)
        {
            Debug.LogError("Add fruits to list in Fruit Spawner!");
            return;
        }

        Instantiate(fruits[Random.Range(0, fruits.Count)],
            new Vector3(fruitPosX, 0f, fruitPosY),
            Quaternion.identity);
    }
}
