using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Build
{


    protected void Awake()
    {
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
        People++;
    }

}
