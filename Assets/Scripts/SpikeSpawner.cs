using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] int maxXPos = 11;
    [SerializeField] int maxYPos = 8;
    [SerializeField] GameObject spikes;
    RaycastHit hitInfo;
    float spawnDelay;
    int spawnedSpikes;
    int maxOfSpikes;

    private void Awake()
    {
        spawnedSpikes = 0;
        StartCoroutine(StartSpawningSpikes());
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
        do
        {
            yield return new WaitForSeconds(spawnDelay);
            if (spawnedSpikes < maxOfSpikes)
            {
                SpawnSpikes();
                spawnedSpikes++;
            }
        } while (true);
        
    }

    private void SpawnSpikes()
    {
        float spikesPosX;
        float spikesPosY;
        do
        {
            spikesPosX = Random.Range(-maxXPos, maxXPos + 1);
            spikesPosY = Random.Range(-maxYPos, maxYPos + 1);

            Physics.Raycast(
                new Vector3(spikesPosX, 5f, spikesPosY),
                Vector3.down,
                out hitInfo);

        } while (!hitInfo.collider.transform.tag.Equals("Background"));

        if (!spikes)
        {
            Debug.LogError("Add spikes in editor to spikes spawner!");
            return;
        }

        Instantiate(spikes, new Vector3(spikesPosX, 0f, spikesPosY), Quaternion.identity);
    }

    public void RemoveSpikes()
    {
        spawnedSpikes--;
    }
}
