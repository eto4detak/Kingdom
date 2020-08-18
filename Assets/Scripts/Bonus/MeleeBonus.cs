using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBonus : MonoBehaviour, IBonus
{
    public float maxBonus = 20;
    public float maxStrongTime = 5f;

    private Unit ownerManager;
    private Bonus unitBonus;
    private float bonusTime;
    private float percentBonus;
    private float damageTime;

    private void Awake()
    {
        ownerManager = GetComponent<Unit>();
        unitBonus.type = TypeBonus.Armor;
        unitBonus.value = 0;
        percentBonus = maxBonus / maxStrongTime;
    }

    private void Start()
    {
        ownerManager.health.EventTakeDamage.AddListener(OnDamage);
    }

    private void FixedUpdate()
    {
        SetBonusTime();
        unitBonus.value = bonusTime * percentBonus;
        if (unitBonus.value > maxBonus) unitBonus.value = maxBonus;
        if (unitBonus.value < 0) unitBonus.value = 0;
    }


    public Bonus GetBonus()
    {
        return unitBonus;
    }
    private void OnDamage()
    {
        damageTime = 1f;
    }
    private void SetBonusTime()
    {
        if (ownerManager.movement.run)
        {
            bonusTime = 0;
            return;
        }

        if (damageTime > 0)
        {
            damageTime -= Time.fixedDeltaTime;
            bonusTime -= 2 * Time.fixedDeltaTime;
            return;
        }
        if (ownerManager.Armament.isAttack)
        {
            bonusTime -= 3 * Time.fixedDeltaTime;
            return;
        }
        bonusTime += Time.fixedDeltaTime;
        if (bonusTime > maxStrongTime) bonusTime = maxStrongTime;
    }

}
