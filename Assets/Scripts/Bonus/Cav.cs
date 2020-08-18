using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cav : MonoBehaviour, IBonus
{
    public float maxBonus = 50;
    public float maxStrongTime = 5f;

    private Unit ownerManager;
    private Bonus unitBonus;
    private float currentRunTime;
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
        if (ownerManager.movement.run)
        {
            currentRunTime += Time.fixedDeltaTime;
            unitBonus.value = currentRunTime * percentBonus;
        }
        else
        {
            currentRunTime = 0;
            unitBonus.value = 0;
        }
        if (unitBonus.value > maxBonus) unitBonus.value = maxBonus;
    }

    public Bonus GetBonus()
    {
        return unitBonus;
    }

}
