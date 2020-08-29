using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitsManager : Singleton<UnitsManager>
{
    [Serializable]
    public struct TeamColor
    {
        public Team team;
        public Material color;
    }
    
    public List<Locality> localities = new List<Locality>();
    public UnityEvent EventPlayerDead = new UnityEvent();
    public UnityEvent EvennEnemyDead = new UnityEvent();
    
    public Material playerMaterial;
    public Material enemyMaterial;
    public Material flickerMaterial;

    private void Start()
    {
        Unit[] units = transform.GetComponentsInChildren<Unit>();
        for (int i = 0; i < units.Length; i++)
        {
            AttachedUnit(units[i]);
        }
    }

    public void ChangeUnitsActivity(bool activity)
    {
        //for (int i = 0; i < builds.Count; i++)
        //{
        //    AttachedUnit(builds[i]);
        //    builds[i].ChangeUnitActivity(activity);
        //}

        //for (int i = 0; i < enemyUnits.Count; i++)
        //{
        //    AttachedUnit(enemyUnits[i]);
        //    enemyUnits[i].ChangeUnitActivity(activity);
        //}
    }

    public List<Unit> GetClosestUnits(Vector3 target)
    {
        List<Unit> party = new List<Unit>();
        //float mindistance = 10f;
        //Vector3 distance;
        //for (int i = 0; i < builds.Count; i++)
        //{
        //    if (builds[i] == null) continue;
        //    if (builds[i].Armament.isAttack) continue;
        //    distance = target - builds[i].transform.position;
        //    distance.y = 0;
        //    if (distance.magnitude < mindistance)
        //    {
        //        party.Add( builds[i]);
        //    }
        //}
        return party;
    }

    public Unit GetClosestFreePlayerUnit(Vector3 target)
    {
        Unit closest = null;
        //float distance = Mathf.Infinity;
        //float mindistance = Mathf.Infinity;
        //for (int i = 0; i < builds.Count; i++)
        //{
        //    if (builds[i] == null) continue;
        //    if (builds[i].Armament.isAttack) continue;
        //    distance = (target - builds[i].transform.position).magnitude;
        //    if (distance < mindistance)
        //    {
        //        mindistance = distance;
        //        closest = builds[i];
        //    }
        //}
        return closest;
    }

    public Unit GetClosestFreePlayerUnit(Vector3 target, float maxDistance)
    {
        Unit closest = null;
        //float distance = Mathf.Infinity;
        //float mindistance = Mathf.Infinity;
        //for (int i = 0; i < builds.Count; i++)
        //{
        //    distance = (target - builds[i].transform.position).magnitude;
        //    if (distance < mindistance && distance < maxDistance)
        //    {
        //        mindistance = distance;
        //        closest = builds[i];
        //    }
        //}
        return closest;
    }

    public List<Unit> GetPlayerFreeUnits()
    {
        List<Unit> freeUnits = new List<Unit>();
        //for (int i = 0; i < builds.Count; i++)
        //{
        //    if (builds[i].isFree) freeUnits.Add(builds[i]);
        //}
        return freeUnits;
    }


    public Material GetTeamColor(Team team)
    {
        if (team == Team.Player1) return playerMaterial;
        return enemyMaterial;
    }


    //public List<Build> GetCommandStack(Team team)
    //{
    //    if (team == Team.Player1) return builds;
    //    else return enemyUnits;
    //}

    //public List<Build> GetEnemyStack(Team team)
    //{
    //    if (team == Team.Player1) return enemyUnits;
    //    else return builds;
    //}

    public void AttachedUnit(Unit unit)
    {
        //List<Unit> stack = GetCommandStack(unit.GetTeam());
        //if (!stack.Exists(x => x.Equals(unit)))
        //{
        //    stack.Add(unit);

        //}
        //unit.die.RemoveListener(RemoveUnit);
        //unit.die.AddListener(RemoveUnit);
    }

    private void RemoveUnit(Unit removed)
    {

        //if(removed.GetTeam() == Team.Player1)
        //{
        //    builds.Remove(removed);
        //    if (builds.Count == 0) EventPlayerDead?.Invoke();
        //}
        //else
        //{
        //    enemyUnits.Remove(removed);
        //    if (enemyUnits.Count == 0) EvennEnemyDead?.Invoke();
        //}
    }


    public List<Unit> GetPlayerUnit(UnitType find)
    {
        List<Unit> units = new List<Unit>();
        //for (int i = 0; i < builds.Count; i++)
        //{
        //    if (builds[i].Armament.AttackType.GetUnitType() == find) units.Add(builds[i]);
        //}
        return units;
    }
    public List<Unit> GetEnemyUnit(UnitType find)
    {
        List<Unit> units = new List<Unit>();
        //for (int i = 0; i < enemyUnits.Count; i++)
        //{
        //    if (enemyUnits[i].Armament.AttackType.GetUnitType() == find) units.Add(enemyUnits[i]);
        //}
        return units;
    }

    public Health GetClosestEnemy(Health origin)
    {
        Unit closest = null;
        //float closestDistanceSqr = Mathf.Infinity;
        //Vector3 directionToTarget;
        //List<Unit> enemies = GetEnemyStack(origin.GetTeam());
        //for (int i = 0; i < enemies.Count; i++)
        //{
        //    directionToTarget = enemies[i].transform.position - origin.transform.position;
        //    float dSqrToTarget = directionToTarget.sqrMagnitude;
        //    if (dSqrToTarget < closestDistanceSqr)
        //    {
        //        closestDistanceSqr = dSqrToTarget;
        //        closest = enemies[i];
        //    }
        //}
        return closest.health;
    }

    public void StopUnits()
    {
        //for (int i = 0; i < enemyUnits.Count; i++)
        //{
        //    enemyUnits[i].Command = new StopCommand(UnitsManager.instance.enemyUnits[i]);
        //}
        //for (int i = 0; i < builds.Count; i++)
        //{
        //    builds[i].Command = new StopCommand(UnitsManager.instance.builds[i]);
        //}
    }


}

