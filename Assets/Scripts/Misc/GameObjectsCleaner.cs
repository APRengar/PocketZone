using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsCleaner : MonoBehaviour
{
    public GameObject objectToDestroy;

    public void DestroySelf()
    {
        Destroy(objectToDestroy);        
    }
}
