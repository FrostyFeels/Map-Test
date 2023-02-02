using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : ScriptableObject
{
    public string AttackName;
    public float damage;
    public virtual void DoAttack(List<EnemyHealth> hit)
    {
        foreach (EnemyHealth _hit in hit)
        {
            _hit.OnHit(damage);
        }
    }

}



