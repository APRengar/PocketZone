using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float yMovementMultiplier = 0.5f; // Коэффициент скорости для оси Y, что бы визуально создавался эффект глубины по Y
    [SerializeField] private Animator animator;
    private WeaponController weaponControler;

    private Vector2 movement;

    [Header("ReadOnly")]
    public Vector2 move;
    public bool isShoot;

    private void Awake() 
    {
        weaponControler = GetComponent<WeaponController>();
    }

    void Update()
    {
        movement = new Vector2(move.x, move.y*yMovementMultiplier).normalized; 
        if (movement.x != 0 && movement.y !=0)
        {
            animator.SetBool("isMoving", true);
        }
        else 
        {
            animator.SetBool("isMoving", false);
        }
        if (isShoot)
        {
            Shooting();
        }
    }

    void FixedUpdate()
    {
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shooting()
    {
        weaponControler.ShootAtTheTarget();
    }

    public WeaponController GetWeaponController()
    {
        return weaponControler;
    }
}
