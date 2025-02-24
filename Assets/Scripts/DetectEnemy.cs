using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    int attack = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.CompareTag("Enemy")
        if (collision.GetComponent<Health>() != null)
        { collision.GetComponent<Health>().Damage(attack);}
    }
}
