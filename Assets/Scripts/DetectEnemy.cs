using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    int attack = 10;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.GetComponent<Health>() != null) && (collider.CompareTag("Enemy")))
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(attack);
        }
    }
}