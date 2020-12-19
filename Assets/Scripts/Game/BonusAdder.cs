using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAdder : Singleton<BonusAdder>
{
    private bool isWall;
    private List<Unit> party = new List<Unit>();


    private void Start()
    {
        isWall = true;
    }


    private void FixedUpdate()
    {
        if (isWall)
        {
            if (Input.GetMouseButtonUp(0))
            {
                TryCreateFormation();
            }
        }
    }


    public void TryCreateFormation()
    {
        int minCountUnit = 5;
        List<Vector3> mousePath = MouseManager.instance.mousePath;
        party.Clear();
        float radius = 1f;
        for (int i = 0; i < mousePath.Count; i++)
        {
            //Unit found = UnitsManager.instance.GetClosestFreePlayerUnit(mousePath[i], radius);
            //if (!found) continue;
            //if (party.Exists(x => x == found)) continue;
            //party.Add(found);
        }

        if (party.Count > minCountUnit)
        {
            FormationsManager.instance.AddFormation(party, new WallBonus());
        }
    }

    private void IsRow()
    {
        Vector3 oldDirection;
        Vector3 direction = Vector3.zero;
        for (int i = 1; i < party.Count; i++)
        {
            oldDirection = direction;
            direction =  party[i].transform.position - party[i - 1].transform.position;

        }
    }


}

