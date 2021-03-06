using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSpawner : MonoBehaviour
{
    [SerializeField] int maxXPos = 11;
    [SerializeField] int maxYPos = 8;
    RaycastHit hitInfo;
    float spawnDelay;
    int spawnedSpikes;
    int maxOfSpikes;

    [SerializeField] GameObject spikes;

    private void Awake()
    {
        GetDifficulty();
        spawnedSpikes = 0;
        StartCoroutine(StartSpawningSpikes());
    }

    private void GetDifficulty()
    {
        switch (PlayerPrefsController.GetDifficulty())
        {
            case 0:
                maxOfSpikes = 10;
                spawnDelay = 6f;
                break;
            case 1:
                maxOfSpikes = 20;
                spawnDelay = 4f;
                break;
            case 2:
                maxOfSpikes = 30;
                spawnDelay = 2f;
                break;
            default:
                break;
        }
    }

    private IEnumerator StartSpawningSpikes()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            if (spawnedSpikes < maxOfSpikes && SpawnedSpikes())
            {
                spawnedSpikes++;
            }
        }

    }

    private bool SpawnedSpikes()
    {
        float spikesPosX;
        float spikesPosY;

        spikesPosX = Random.Range(-maxXPos, maxXPos + 1);
        spikesPosY = Random.Range(-maxYPos, maxYPos + 1);

        Physics.Raycast(
            new Vector3(spikesPosX, 5f, spikesPosY),
            Vector3.down,
            out hitInfo);

        if (hitInfo.collider.transform.tag.Equals("Background"))
        {
            Instantiate(spikes, new Vector3(spikesPosX, 0f, spikesPosY), Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveSpikes()
    {
        spawnedSpikes--;
    }
}
