using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Respect : IComparable<Respect>
{
    public Team team;
    public float val;

    public int CompareTo(Respect comparePart)
    {
        return -this.val.CompareTo(comparePart.val);
    }

    public void SetRespect( float respect)
    {
        val = respect;
    }

}

public class Loyalty
{
    public List<Respect> respects = new List<Respect>();

    public void Development(float val)
    {
        float totalCult = 0;
        for (int i = 0; i < respects.Count; i++)
        {
            totalCult += respects[i].val;
        }
        if (totalCult == 0) return;
        for (int i = 0; i < respects.Count; i++)
        {
            float vRespect = respects[i].val + val * (respects[i].val / totalCult);
            respects[i] = new Respect() { team = respects[i].team, val = vRespect };
        }
    }

    public Team Merger(Loyalty newLoyalty)
    {
        for (int i = 0; i < newLoyalty.respects.Count; i++)
        {
            ChangeLoyalty(newLoyalty.respects[i]);
        }
        return respects[0].team;
    }

    public void ChangeLoyalty(Respect added)
    {
        Change(added);
        respects.Sort();
        
    }

    public void ChangeLoyalty(Loyalty newloy)
    {
        List<Respect> newRespects = new List<Respect>();
        newRespects.AddRange(newloy.respects);
        newRespects.AddRange(respects);
        float totalNewLoya = 0;
        float total = 100f;
        float coeff;
        for (int i = 0; i < newRespects.Count; i++)
        {
            totalNewLoya += newRespects[i].val;
        }
        coeff =  total / totalNewLoya;
        for (int i = 0; i < newRespects.Count; i++)
        {
            newRespects[i].SetRespect(newRespects[i].val * coeff);
        }

        foreach (var added in newloy.respects)
        {
            Change(added);
        }

        respects.Sort();

    }

    private void Change(Respect added)
    {


        int index = GetIndexLoyalty(added.team);
        if (index == -1)
        {
            respects.Add(added);
        }
        else
        {
            respects[index] = new Respect() { team = added.team, val = respects[index].val + added.val };
        }
    }

    private int GetIndexLoyalty(Team find)
    {
        for (int i = 0; i < respects.Count; i++)
        {
            if (find == respects[i].team) return i;
        }
        return -1;
    }
}
