using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristic
{
    List<IBonus> sourse = new List<IBonus>();

    public Characteristic(List<IBonus> _sourse)
    {
        sourse = _sourse;
    }

    public Bonus GetBonus(TypeBonus find)
    {
        Bonus bonus = new Bonus();
        bonus.type = find;
        Bonus temp;

        for (int i = 0; i < sourse.Count; i++)
        {
            temp = sourse[i].GetBonus();
            if (temp.type == find)
            {
                bonus.value += temp.value;
            }
        }
        return bonus;
    }


    public void AddBonus(IBonus bonus)
    {
        sourse.Add(bonus);
    }
    public void RemoveBonus(IBonus bonus)
    {
        sourse.Remove(bonus);
    }

}

public enum TypeBonus
{
    Attack,
    Armor
}