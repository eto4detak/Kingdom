using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitsManager : Singleton<UnitsManager>
{
    [System.Serializable]
    public struct TeamColor
    {
        public Team team;
        public Material color;
    }
    
    public List<Locality> localities = new List<Locality>();
    public List<GameUnit> units = new List<GameUnit>();
    public UnityEvent EventPlayerDead = new UnityEvent();
    public UnityEvent EvennEnemyDead = new UnityEvent();
    
    public Material playerMaterial;
    public Material enemyMaterial;
    public Material flickerMaterial;


    protected void Start()
    {
        GameTime.instance.timeChanged.AddListener(TryChangeTeam);
    }



    public List<Locality> GetClosestLocalities(GameObject from)
    {
        List<Locality> locs = new List<Locality>(localities);
        Locality temp;
        for (int j = 0; j < locs.Count; j++)
        {
            float dist1 = Vector3.Distance(from.transform.position, locs[j].transform.position);
            for (int i = 0; i < locs.Count; i++)
            {
                if (locs[j] == locs[i]) continue;

                float dist2 = Vector3.Distance(from.transform.position, locs[i].transform.position);
                if(dist1 < dist2)
                {
                    temp = locs[j];
                    locs[j] = locs[i];
                    locs[i] = temp;
                }
            }
        }

        return locs;
    }

    public Team GetNewTeam()
    {
        Team found = Team.Neitral;
        for (int i = 0; i < localities.Count; i++)
        {
            if((localities[i].team) > found)
            {
                found = localities[i].team;
            }
        }
        return found +1;
    }

    public Material GetTeamColor(Team team)
    {
        if (team == Team.Player1) return playerMaterial;
        return enemyMaterial;
    }

    protected void TryChangeTeam()
    {
        int minUnit = 50;
        Team noTeam = Team.Neitral;
        if (GameTime.instance.timeDay == TimeDay.Day)
        {
            for (int i = 0; i < localities.Count; i++)
            {
               if(localities[i].team == noTeam &&
                    localities[i].people.humans.Count > minUnit)
                {
                    localities[i].ChangeTeam(GetNewTeam());
                }
            }
        }
    }

    public void Registration(GameUnit add)
    {
        //List<Unit> stack = GetCommandStack(unit.GetTeam());
        //if (!stack.Exists(x => x.Equals(unit)))
        //{
        //    stack.Add(unit);

        //}
        //unit.die.RemoveListener(RemoveUnit);
        //unit.die.AddListener(RemoveUnit);
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


}

