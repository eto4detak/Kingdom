using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch : MonoBehaviour, IBonus
{
    public float maxBonus = 20;
    public float maxStrongTime = 5f;

    private Unit ownerManager;
    private Bonus unitBonus;
    private float bonusTime;
    private float percentBonus;

    private void Awake()
    {
        ownerManager = GetComponent<Unit>();
        unitBonus.type = TypeBonus.Attack;
        unitBonus.value = 0;
        percentBonus = maxBonus / maxStrongTime;
    }

    private void FixedUpdate()
    {
        UpBonus();
    }

    public void UpBonus()
    {
        if (ownerManager.movement.run) bonusTime = 0;
        else bonusTime += Time.deltaTime;

        unitBonus.value = bonusTime * percentBonus;
        if (unitBonus.value > maxBonus) unitBonus.value = maxBonus;
        if (unitBonus.value < 0) unitBonus.value = 0;
    }

    public Bonus GetBonus()
    {
        return unitBonus;
    }

}
