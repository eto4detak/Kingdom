using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBonus : IBonus
{
    private Bonus plus = new Bonus { type = TypeBonus.Armor, value = 10f };
    private float bonusTime;
    private float percentBonus;

    public Bonus GetBonus()
    {
        return plus;
    }
}
