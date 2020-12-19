using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locality : GameUnit
{
    public void ChangeTeam(Team newTeam)
    {
        people.SetTeam(newTeam);
    }

    protected T CreateFormation<T>(int countPeople, Team currentTeam) where T : GameUnit
    {
        List<Human> outPeople = people.Leave(countPeople, currentTeam);
        if (outPeople == null) return null;

        var prefab = GetPrefab<T>();
        T formation = Instantiate(prefab, transform.position, Quaternion.identity);
        formation.people.Join(outPeople);
        return formation;
    }

    protected T GetPrefab<T>() where T : MonoBehaviour
    {
        return Resources.Load<T>("Units/" + typeof(T));
    }

}

