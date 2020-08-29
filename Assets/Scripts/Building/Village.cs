using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Locality
{
    protected override void Awake()
    {
        base.Awake();
        AddPotential(new GameUnitInfo { name = "Immigrants", start = CreateImmigrants });
    }

    public void CreateImmigrants()
    {
        int minPeople = 20;
        int immigra = 20;
        if (People < minPeople + immigra) return;

        People -= immigra;
        culture.ChangeCulture(-immigra);
        var prefab = GetPrefab<Immigrants>();
        Immigrants immigrants = Instantiate(prefab, transform.position, Quaternion.identity);
        immigrants.Setup(this);
        immigrants.Select();
    }

    protected override void Development()
    {
        float dev = 1f;
        People += (int)dev;
        culture.ChangeCulture(dev);
    }

}
