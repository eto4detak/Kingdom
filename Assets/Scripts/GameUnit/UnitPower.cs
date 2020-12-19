using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPower
{
    private int force;


    public void SetPower(int _force)
    {
        force = 0;
        ChangePower(_force);
    }

    public void ChangePower(int change)
    {
        force += change;
        if (force < 0) force = 0;
    }

    public int GetPower()
    {
        return force;
    }

}
