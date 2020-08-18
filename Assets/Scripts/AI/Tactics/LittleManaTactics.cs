using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleManaTactics : IAITactics
{
    public float need;

    private Unit owner;
    private float currentTime;
    private float turnTime = 2f;

    private bool closeBorder;

    public LittleManaTactics()
    {
    }

    public void Control()
    {

    }

    public float CheckNeed()
    {
        float littleMana = 30f;
        float maxNeed = 7;

        //if (owner.Mana < littleMana)
        //{
        //    need = maxNeed;
        //}
        //else
        //{
        //    need =  owner.maxMana / owner.Mana - 1;
        //}
        return need;
    }
}
