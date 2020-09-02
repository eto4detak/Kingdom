using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Locality
{
    protected override void Awake()
    {
        base.Awake();

        loyalty.ChangeLoyalty(new Respect() { team = team, val = 100 });
        AddPotential(new GameUnitInfo { name = "Immigrants", start = CreateImmigrants });
    }

    public void CreateImmigrants()
    {
        int people = 20;
        var immigrants = CreateFormation<Immigrants>(people);
        if (immigrants)
        {
            immigrants.Setup(this);
            immigrants.Select();
        }
    }

    protected override void Development()
    {
        float dev = 1f;
        People += (int)dev;
        culture.Development(dev);
        loyalty.Development(dev);
    }

}
