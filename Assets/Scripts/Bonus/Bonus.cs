using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Bonus
{
    public TypeBonus type;
    public float value;
}

public interface IBonus
{
    Bonus GetBonus();
}

