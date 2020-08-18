using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionTactics : IAITactics
{
    public float need;

    private Unit owner;
    private float currentTime;
    private float turnTime = 2f;

    private bool closeBorder;

    public EvasionTactics()
    {
    }

    public void Control()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.fixedDeltaTime;
        }
        else
        {
            currentTime = turnTime;
        }

        //owner.Move(owner.transform.position * -1);
    }

    public float CheckNeed()
    {
        float maxDistance = 10f;
        float evasionNeed = 5f;
        //Vector3 endDistance = owner.transform.position;

        //if (endDistance.magnitude > maxDistance)
        //{
        //    need = evasionNeed;
        //}
        //else
        //{
        //    need = 0;
        //}
        return need;
    }
}
