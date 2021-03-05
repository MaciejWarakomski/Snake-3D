using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    Movement movementScript;
    FruitSpawner fruitSpawnerScript;

    private void Awake()
    {
        movementScript = GetComponent<Movement>();
        fruitSpawnerScript = FindObjectOfType<FruitSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fruit":
                Destroy(other.gameObject);
                movementScript.AddSnakeSize();
                StartCoroutine(fruitSpawnerScript.StartSpawningFruit());
                break;
            case "Spikes":
                Destroy(other.gameObject);
                movementScript.RemoveSnakeSize();
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
