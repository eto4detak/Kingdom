using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominanceTactics : IAITactics
{
    public float need;

    private Unit owner;
    private float currentTime;
    private float turnTime = 2f;
    private bool closeBorder;

    public DominanceTactics()
    {
    }

    public void Control()
    {
        //float optimaDist = 7f;

        //if(owner.toTarget.magnitude > optimaDist)
        //{
        //    Vector3 direct = owner.target.transform.position - owner.transform.position;
        //    owner.Move(direct);
        //}
        //owner.ApplyAttackSpell();
    }

    public float CheckNeed()
    {
        float fourth = 0.25f;
        float maxNeed = 8f;
        need = 0;

        //bool manyMana = owner.Mana > owner.maxMana * fourth;

        //if (manyMana)
        //{
        //    need = maxNeed;
        //}
        return need;
    }
}