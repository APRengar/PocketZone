using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyBodyTarget;
    public GameObject Selector;
    [SerializeField] Animator animator;


    [Header("Targeting")]
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistanceThreshlod;
    private float distanceToTarget;
    [SerializeField] float myRange = 12;

    private Transform target;
    private bool playerInRange;
    private EneemyAttack enemyAttack;

    private Health myHealth;
    [SerializeField] SpriteGroupAlphaOnDeath mySprites;
    private bool AIActive = true;

    private Rigidbody2D rb;


    private void Awake() 
    {
        myHealth = GetComponent<Health>();
        enemyAttack = GetComponent<EneemyAttack>();
        rb = GetComponent<Rigidbody2D>();
        AIActive = true;
    }

    private void OnEnable() 
    {
        myHealth.playerDeing += Death;
    }
    private void OnDisable() 
    {
        myHealth.playerDeing -= Death;
    }

    private void Start() 
    {
        path = GetComponent<AIPath>();
        path.maxSpeed = moveSpeed;
        target = Player.Instance.transform;
    }

    private void Update() 
    {
        if (!AIActive)
            return;
        distanceToTarget  = Vector2.Distance(transform.position, target.position);
        // Vector3 direction = (target.position - transform.position).normalized;
        if (distanceToTarget < myRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (target && playerInRange)
        {
            
            // Debug.Log(distanceToTarget);

            if (distanceToTarget < stopDistanceThreshlod)
            {
                // rb.velocity = Vector3.zero;
                path.destination = transform.position;
                animator.SetBool("isMoving", false);
                // Debug.Log("Stop");
                enemyAttack.Attack();
            }
            else
            {
                // rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
                path.destination = target.position;
                animator.SetBool("isMoving", true);
                // Debug.Log("Follow");
            }
        }
        else
        {
            path.destination = transform.position;
            animator.SetBool("isMoving", false);
        }
    }

    // private void OnTriggerStay2D(Collider2D other) 
    // {
    //     if (!AIActive)
    //         return;
    //     if (other.tag == "Player")
    //     {
    //         target = other.transform;
    //         playerInRange = true;

    //     }
    // }
    // private void OnTriggerExit2D(Collider2D other) 
    // {
    //     if (!AIActive)
    //         return;
    //     if (other.tag == "Player")
    //     {
    //         target = null;
    //         playerInRange = false;
    //     }
    // }

    public void ActivateSelector()
    {
        Selector.SetActive(true);
    }

    public void DeactivateSelector()
    {
        Selector.SetActive(false);
    }

    public Transform GetAim()
    {
        return enemyBodyTarget;
    }

    private void Death()
    {
        AIActive = false;
        
        rb.velocity = Vector3.zero;
        path.StopAllCoroutines();
        path.destination = transform.position;
        mySprites.StartFade(0, 1);
        GetComponentInChildren<EnemyLoot>().DropLoot();
    }
}
