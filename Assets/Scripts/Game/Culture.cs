using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CultName
{
    Ra,
    Shan,
    Elin
}

[Serializable]
public class Cult : IComparable<Cult>
{
    public CultName name;
    public float val;

    public int CompareTo(Cult comparePart)
    {
        if (comparePart == null)
            return 1;

        else
            return -this.val.CompareTo(comparePart.val);
    }
}

public class Culture : MonoBehaviour
{
    public List<Cult> cults = new List<Cult>();
    public UnityEvent changedCulture = new UnityEvent();


    public void Development(float val)
    {
        float totalCult = 0;
        for (int i = 0; i < cults.Count; i++)
        {
            totalCult += cults[i].val;
        }
        if (totalCult == 0) return;
        for (int i = 0; i < cults.Count; i++)
        {
            cults[i].val += val * (cults[i].val / totalCult);
        }
    }

    public void ChangeCult(Cult added)
    {
        Cult cult = GetCult(added.name);
        if (cult == null)
        {
            cults.Add(new Cult() { name = added.name, val = added.val });
        }
        else
        {
            cult.val += added.val;
        }
        cults.Sort();
    }

    public bool Merger(Culture newCulture)
    {
        CultName first = cults[0].name;
        for (int i = 0; i < newCulture.cults.Count; i++)
        {
            ChangeCult(newCulture.cults[i]);
            newCulture.cults[i].val = 0;
        }
        CultName now = cults[0].name;

        if (now != first)
        {
            changedCulture?.Invoke();
            return true;
        }
        return false;
    }

    public Cult GetCult(CultName find)
    {
        for (int i = 0; i < cults.Count; i++)
        {
            if (find == cults[i].name) return cults[i];
        }
        return null;
    }

}
