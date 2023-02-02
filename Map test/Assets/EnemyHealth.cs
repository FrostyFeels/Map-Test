using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float armor;

    public void OnHit(float dmg)
    {
        health -= (dmg - armor);
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
