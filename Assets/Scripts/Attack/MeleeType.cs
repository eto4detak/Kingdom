using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeType : AttackType
{
    private void Awake()
    {
        type = UnitType.melee;
    }

    public override void Attack(Health enemy)
    {
        if (enemy != null && owner != null) LookAtTarget(enemy, owner);
        enemy.TakeDamage(damage + bonusDamage);
    }
}