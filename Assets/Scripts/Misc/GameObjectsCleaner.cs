using UnityEngine;

public class GameObjectsCleaner : MonoBehaviour
{
    public GameObject objectToDestroy;

    public void DestroySelf()
    {
        Destroy(objectToDestroy);        
    }
}
