using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    SnakeSizeControl snakeSizeControlScript;
    FruitSpawner fruitSpawnerScript;

    private void Awake()
    {
        snakeSizeControlScript = GetComponent<SnakeSizeControl>();
        fruitSpawnerScript = FindObjectOfType<FruitSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fruit":
                Destroy(other.gameObject);
                snakeSizeControlScript.AddSnakeSize();
                StartCoroutine(fruitSpawnerScript.StartSpawningFruit());
                break;
            case "Spikes":
                Destroy(other.gameObject);
                snakeSizeControlScript.RemoveSnakeSize();
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
