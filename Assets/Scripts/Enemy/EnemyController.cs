using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public GameObject Selector;

    [SerializeField] private Transform enemyBodyTarget;
    [SerializeField] Animator animator;

    [Header("Targeting")]
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistanceThreshlod;
    [SerializeField] float myRange = 12;

    [SerializeField] SpriteGroupAlphaOnDeath mySprites;

    private Transform target;
    private bool playerInRange;
    private EneemyAttack enemyAttack;
    private Health myHealth;
    private float distanceToTarget;
    private bool AIActive = true;

    private void Awake() 
    {
        myHealth = GetComponent<Health>();
        enemyAttack = GetComponent<EneemyAttack>();
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
        if (!AIActive) return;

        distanceToTarget  = Vector2.Distance(transform.position, target.position);
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
            if (distanceToTarget < stopDistanceThreshlod)
            {
                path.destination = transform.position;
                animator.SetBool("isMoving", false);
                enemyAttack.Attack();
            }
            else
            {
                path.destination = target.position;
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            path.destination = transform.position;
            animator.SetBool("isMoving", false);
        }
    }

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
        path.StopAllCoroutines();
        path.destination = transform.position;
        mySprites.StartFade(0, 1);
        GetComponentInChildren<EnemyLoot>().DropLoot();
    }
}
