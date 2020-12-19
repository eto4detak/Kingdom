using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class People
{
    public List<Human> humans = new List<Human>();
    public Culture culture = new Culture();
    [HideInInspector]
    public UnityEvent peopleChanged = new UnityEvent();

    public People()
    {
        culture.SetOrigin(humans);
    }

    public void Development(int dev)
    {
        if (humans.Count> 0)
        {
            for (int i = 0; i < dev; i++)
            {
                Team mainTeam = FindMainTeam();
                Human found = humans.Find(x => x.team == mainTeam);
                humans.Add(found.ShallowCopy());
            }
        }
        else
        {
            throw new Exception("erorr people");
        }
        Change();
    }

    public void AddHuman(Human human)
    {
        humans.Add(human);
        Change();
    }

    public void KillHumans(int killCount)
    {
        if (killCount == 0) return;
        if (killCount < 1)
        {
            throw new Exception("bad KillHumans");
        }

        int startKill = humans.Count - killCount;
        if (startKill > -1)
        {
            humans.RemoveRange(startKill, killCount);
        }
        else
        {
            humans.Clear();
        }
        Change();
    }

    public List<Human> Leave(int needCount, Team find)
    {
        List<Human> outgoing = new List<Human>();
        List<Human> found = FindTeamHumans(needCount, find);
        for (int i = 0; i < found.Count; i++)
        {
            humans.Remove(found[i]);
        }
        outgoing.AddRange(found);
        Change();
        return outgoing;
    }

    public List<Human> FindTeamHumans(int needCount, Team find)
    {
        int minCount = needCount + 1;
        List<Human> found = humans.FindAll(x => x.team == find);
        if (found.Count < minCount) return found;
        return found.GetRange(0, needCount);
    }


    public bool Join(People joins)
    {
        return Join(joins.humans);
    }

    public bool Join(List<Human> joins)
    {
        List<Human> newHumans = new List<Human>();
        newHumans.AddRange(humans);
        newHumans.AddRange(joins);
        humans = newHumans;

        Change();
        return false;
    }

    public void Change()
    {
        culture.Refresh();
        peopleChanged?.Invoke();
    }

    public Team FindMainTeam()
    {
        Team main = Team.Neitral;
        int maxCount = 0;

        List<Team> enableTeams = GetTeams();
        for (int i = 0; i < enableTeams.Count; i++)
        {
            int count = humans.FindAll(x => x.team == enableTeams[i]).Count;
            if(count > maxCount)
            {
                maxCount = count;
                main = enableTeams[i];
            }
        }

        return main;
    }

    public List<TeamValue> GetTeamStatistic()
    {
        List<TeamValue> teams = new List<TeamValue>();

        List<Team> enableTeams = GetTeams();
        for (int i = 0; i < enableTeams.Count; i++)
        {
            List<Human> found = humans.FindAll(x => x.team == enableTeams[i]);
            teams.Add(new TeamValue() { team = enableTeams[i], count = found.Count });
        }

        return teams;
    }

    public List<Team> GetTeams()
    {
        List<Team> enableTeams = new List<Team>();
        for (int i = 0; i < humans.Count; i++)
        {
            bool added = enableTeams.Exists(x => x == humans[i].team);
            if (!added)
            {
                enableTeams.Add(humans[i].team);
            }
        }
        return enableTeams;
    }

    public void SetTeam(Team newTeam)
    {
        for (int i = 0; i < humans.Count; i++)
        {
            humans[i].team = newTeam;
        }
        Change();
    }


    protected void SetCulture(CultName cult)
    {
        for (int i = 0; i < humans.Count; i++)
        {
            humans[i].cult.name = cult;
        }
        Change();
    }


}

public class TeamValue
{
    public Team team;
    public int count;
}