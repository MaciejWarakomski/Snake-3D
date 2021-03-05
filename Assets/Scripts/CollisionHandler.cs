using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    Movement movementScript;
    

    private void Awake()
    {
        movementScript = GetComponent<Movement>();
        
    }

    
}
