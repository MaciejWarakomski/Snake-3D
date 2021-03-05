using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] int maxXPos = 11;
    [SerializeField] int maxYPos = 8;
    [SerializeField] int maxOfSpikes = 5;
    [SerializeField] float spawnDelay = 5f;
    [SerializeField] GameObject spikes;
    RaycastHit hitInfo;
    float spawnTimer;
    int spawnedSpikes;

    // Start is called before the first frame update
    void Start()
    {
        spawnedSpikes = 0;
        StartCoroutine(StartSpawningSpikes());
    }

    private IEnumerator StartSpawningSpikes()
    {
        do
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnSpikes();
            spawnedSpikes++;
        } while (spawnedSpikes <= maxOfSpikes);
        
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
                new Vector3(spikesPosX, 2f, spikesPosY),
                Vector3.down,
                out hitInfo);

        } while (hitInfo.collider.transform.tag.Equals("Background"));

        if (!spikes)
        {
            Debug.LogError("Add spikes in editor to spikes spawner!");
            return;
        }

        Instantiate(spikes, new Vector3(spikesPosX, 0f, spikesPosY), Quaternion.identity);
    }
}
