using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CultName
{
    Neitral,
    Ra,
    Shan,
    Elin
}

[Serializable]
public struct Cult : IComparable<Cult>
{
    public CultName name;
    public float val;

    public int CompareTo(Cult comparePart)
    {
        return -this.val.CompareTo(comparePart.val);
    }

    public void Change(float added)
    {
        val = val + added;
    }

}

[Serializable]
public class Culture
{
    public List<Cult> cults = new List<Cult>();
    [HideInInspector]
    public UnityEvent changedCulture = new UnityEvent();
    private List<Human> origin;

    public void SetOrigin(List<Human> _origin)
    {
        origin = _origin;
    }


    public void Refresh()
    {
        cults.Clear();
        for (int i = 0; i < origin.Count; i++)
        {
            AddCult(origin[i].cult);
        }
    }

    protected void AddCult(Cult added)
    {
        int cult = cults.FindIndex(x => x.name == added.name);
        if (cult == -1)
        {
            cults.Add(added);
        }
        else
        {
            var newCults = cults[cult];
            newCults.Change(added.val);
            cults[cult] = newCults;
        }
        cults.Sort();
    }

}
