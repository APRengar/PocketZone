using System.Collections;
using UnityEngine;

public class EneemyAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float attackIntervals;
    [SerializeField] int damage;

    private bool isAttacking = false;
    
    public void Attack()
    {
        // Prevent overlapping attacks
        if (isAttacking) return;

        // Safety check for the player instance
        if (Player.Instance == null)
        {
            Debug.LogWarning("Player instance is null. Cannot perform attack.");
            return;
        }
        // Check if the player has a Health component
        Health playerHealth = Player.Instance.GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogWarning("Player does not have a Health component. Cannot perform attack.");
            return;
        }
        // Check if the animator is assigned
        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned. Cannot play attack animation.");
            return;
        }

        // Start the attack coroutine
        StartCoroutine(AttackCoro(playerHealth));
    }

    private IEnumerator AttackCoro(Health playerHealth)
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        playerHealth.TakeDamage(damage);
        yield return new WaitForSeconds(attackIntervals);
        isAttacking = false;
    }
}
