using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationsManager : Singleton<FormationsManager>
{
    private List<Formation> allFormations = new List<Formation>();

    protected void FixedUpdate()
    {
        CheckFormations();
    }
    public void DieUnit(Unit dead)
    {
        RemoveInFormations(dead);
    }

    public void AddFormation(List<Unit> party, IBonus bonus)
    {
        List<Unit> copyParty = new List<Unit>(party);
        if (ExistUnitsInFormation(copyParty)) return;

        Formation formation = new Formation(copyParty, bonus);
        allFormations.Add(formation);
    }

    public bool ExistUnitsInFormation(List<Unit> party)
    {
        for (int i = 0; i < allFormations.Count; i++)
        {
            if (allFormations[i].ExistUnits(party)) return true;
        }
        return false;
    }

    public Formation FindFormation(Unit find)
    {
        for (int i = 0; i < allFormations.Count; i++)
        {
            if (allFormations[i].InForamtion(find))
            {
                return allFormations[i];
            }
        }
        return null;
    }

    public int GetCount()
    {
        return allFormations.Count;
    }


    private void CheckFormations()
    {
        for (int i = 0; i < allFormations.Count; i++)
        {
            allFormations[i].CheckFormation();
        }
    }

    private void RemoveInFormations(Unit dead)
    {
        for (int i = 0; i < allFormations.Count; i++)
        {
            allFormations[i].RemoveUnit(dead);
        }
    }

}



public class Formation
{
    public List<Unit> party = new List<Unit>();
    public IBonus bonus;

    public Formation(List<Unit> p_party, IBonus p_bonus)
    {
        party = p_party;
        bonus = p_bonus;
        SetFormation();

        for (int i = 0; i < party.Count; i++)
        {
            party[i].AddBonus(bonus);
        }
    }

    public void RemoveUnit(Unit removed)
    {
        party.Remove(removed);
    }

    public bool InForamtion(Unit find)
    {
        return party.Exists(x => x == find);
    }

    public bool ExistUnits(List<Unit> units)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (party.Exists(x => x == units[i])) return true;
        }
        return false;
    }

    public void CheckFormation()
    {
        if (party.Count < 2) return;
        Vector3 s = party[party.Count - 1].transform.position - party[0].transform.position;

        for (int i = 0; i < party.Count; i++)
        {
            Vector3 mom1 = party[i].transform.position - party[0].transform.position;
            Vector3 cros = Vector3.Cross(mom1, s);
            float distance = Vector3.Dot(cros, s);
        }

    }

    private void SetFormation()
    {
        float minDistance = 3f;
        if (party.Count == 0) return;
        Vector3 center = Vector3.zero;
        for (int i = 0; i < party.Count; i++)
        {
            center += party[i].transform.position;
        }
        center = center / party.Count;

        Vector3 lookDirect = Vector3.Cross(Vector3.up, party[0].transform.forward).normalized;
        for (int i = 0; i < party.Count; i++)
        {
            party[i].MoveTo(party[0].transform.position + lookDirect * i * minDistance );
        }
    }



}