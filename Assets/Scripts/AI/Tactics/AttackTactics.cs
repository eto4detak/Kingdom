using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTactics : IAITactics
{
    public float need;

    private Unit owner;
    public AttackTactics()
    {
    }

    public void Control()
    {
        //float maxOffset = 1f;
        //Vector3 toTarget = owner.target.transform.position - owner.transform.position;
        //Vector3 forvard = owner.transform.forward * toTarget.magnitude;
        //float delta = (forvard - toTarget).magnitude;
        //if(delta < maxOffset) owner.ApplyAttackSpell();
    }

    public float CheckNeed()
    {
        float val = 1f;

        need = val;
        return need;
    }

}
