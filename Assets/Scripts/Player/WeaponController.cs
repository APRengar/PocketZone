using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] CircleCollider2D weaponRange;
    [SerializeField] Transform aimer;

    [Header("ReadOnly")]
    [SerializeField] EnemyController selectedTarget;
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] int selectedEnemyID;

    private float upperArmAngle = 35;

    private void FixedUpdate() 
    {
        Aim();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            enemies.Add(other.GetComponent<EnemyController>());
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            other.GetComponent<EnemyController>().DeactivateSelector();
            selectedTarget = null;
            enemies.Remove(other.GetComponent<EnemyController>());
        }
    }


    private void Aim()
    {
        if (enemies.Count <= 0)
        {
            selectedTarget = null;
            aimer.localRotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        if (!selectedTarget)
        {
            selectedTarget = enemies[0];
            selectedEnemyID = 0;
            selectedTarget.ActivateSelector();
        }
        else
        {
            Vector3 localEnemyPosition = aimer.parent.InverseTransformPoint(selectedTarget.GetAim().position);
            Vector3 localDirection = (localEnemyPosition - aimer.localPosition).normalized;
            float angle = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg;
            aimer.localRotation = Quaternion.Euler(0, 0, angle + upperArmAngle);
        }
    }

    public void ChangeTarget()
    {
        selectedTarget.DeactivateSelector();
        int i = selectedEnemyID + 1;
        if (i < enemies.Count)
        {
            selectedTarget.DeactivateSelector();
            selectedTarget = enemies[selectedEnemyID + 1];
            selectedEnemyID = selectedEnemyID + 1;
            selectedTarget.ActivateSelector();
        }
        else
        {
            selectedTarget = enemies[0];
            selectedEnemyID = 0;
            selectedTarget.ActivateSelector();
        }
    }


    public void ShootAtTheTarget()
    {

    }
}
