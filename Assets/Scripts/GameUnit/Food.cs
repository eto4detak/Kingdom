using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food
{
    private int quantity;
    private int growth;
    public UnityEvent foodChanged = new UnityEvent();
    public UnityAction<int> hunger;

    public int Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
            if (quantity < 0)
            {
                Hunger(quantity * (-1));
                quantity = 0;
            }
        }
    }

    public int Growth { get => growth; set => growth = value; }


    public void Change(int val)
    {
        Quantity += val;
        foodChanged?.Invoke();
    }


    public void Hunger(int need)
    {
        hunger.Invoke(need);
    }


}
