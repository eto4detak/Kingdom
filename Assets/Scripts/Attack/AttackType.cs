using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    public float damage;
    public float bonusDamage;

    protected float rotSpeed = 10f;
    protected UnitType type;
    protected Health owner;
    protected CharacterAttack armammert;

    private void Start()
    {
        owner = GetComponent<Health>();
        armammert = GetComponent<CharacterAttack>();
    }

    public virtual void Attack(Health enemy)
    {
    }

    public UnitType GetUnitType()
    {
        return type;
    }

    protected void LookAtTarget(Health enemy, Health owner)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(new Vector3(armammert.attackDirection.x, 0, armammert.attackDirection.z)), rotSpeed * Time.deltaTime);
    }

    public virtual void DoType()
    {

    }
}

public enum UnitType
{
    melee,
    cavalery,
    archer,
}