using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human 
{
    public Team team;
    public Cult cult = new Cult {val = 1 };

    public Human ShallowCopy()
    {
        return (Human)this.MemberwiseClone();
    }


}
