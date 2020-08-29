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
        int minPeople = 40;
        int countImmigrants = 20;
        if (People < minPeople) return;

        People -= countImmigrants;
        var prefab = GetPrefab<Immigrants>();
        Immigrants immigrants = Instantiate(prefab, transform.position, Quaternion.identity);
        immigrants.Setup(this);
        immigrants.Select();
    }

    protected override void Development()
    {
        float dev = 1f;
        People += (int)dev;
        culture.Development(dev);
    }

}
