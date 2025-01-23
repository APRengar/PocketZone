using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletVelocity = 20;
    private Rigidbody2D rb;
    private int bulletDamage;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        rb.velocity = transform.right * bulletVelocity;
    }

    public void SetDamage(int newDamage)
    {
        bulletDamage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "DamageReciever")
        {
            other.GetComponentInParent<EnemyController>().GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(gameObject);
            return;
        }
        else if (other.tag == "Obstacle")
        {
            Destroy(gameObject);
            return;
        }
    }
}
