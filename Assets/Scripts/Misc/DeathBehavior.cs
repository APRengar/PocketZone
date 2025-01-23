using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehavior : MonoBehaviour
{
    [SerializeField] Health myHealth;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() 
    {
        myHealth.playerDeing += Death;
    }
    private void OnDisable() 
    {
        myHealth.playerDeing -= Death;
    }
    
    private void Death()
    {
        animator.SetTrigger("Death");
    }
}
