using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Weapon/Attack")]
public class WeaponShot : Attack
{
    public float damageFallOff;
    public float rangeFallOff;
    public float range;

    public int shotsPerSelect;
    public int targetAmount;
    public int pierceAmount;
    public float accuracy;

    
    public override void DoAttack(List<EnemyHealth> hit)
    {
        base.DoAttack(hit);
    }

}
