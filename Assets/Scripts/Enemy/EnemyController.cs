using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyBodyTarget;

    public GameObject Selector;
    
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

}
