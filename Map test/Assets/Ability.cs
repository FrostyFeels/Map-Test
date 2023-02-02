using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Ability/Attack")]
public class Ability : Attack
{
    public Ability sideEffect = null;

    public override void DoAttack(List<EnemyHealth> hit)
    {
        base.DoAttack(hit);
    }
}
