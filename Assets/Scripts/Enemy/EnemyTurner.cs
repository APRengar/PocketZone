using UnityEngine;

public class EnemyTurner : MonoBehaviour
{
    [SerializeField] Transform modelFace;
    private Transform target;

    private void Update() 
    {
        target = Player.Instance.transform;
        float direction = target.position.x - transform.position.x;
        // Flip the sprite based on the direction
        if (direction < 0)
        {
            modelFace.localRotation = Quaternion.Euler(0, 180, 0); // Player is on the left
        }
        else if (direction > 0)
        {
            modelFace.localRotation = Quaternion.Euler(0, 0, 0); // Player is on the right
        }
    }
}
