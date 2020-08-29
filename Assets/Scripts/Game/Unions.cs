using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unions : Singleton<Unions>
{
    [Serializable]
    public struct p_unions
    {
        public string Name;
        public Team Team1;
        public Team Team2;
        public TeamState Union;
    }

    public List<p_unions> _Unions = new List<p_unions>();
    private TeamState notFind = TeamState.Neitrals;

    public TeamState GetState(Team _team1, Team _team2)
    {
        for (int i = 0; i < _Unions.Count; i++)
        {
            if ((_Unions[i].Team1.Equals(_team1) && _Unions[i].Team2.Equals(_team2))
                || (_Unions[i].Team1.Equals(_team2) && _Unions[i].Team2.Equals(_team1)))
            {
                return _Unions[i].Union;
            }
        }
        return notFind;
    }

    public bool CheckAllies(Team _team1, Team _team2)
    {
        return _team1 == _team2
            || GetState(_team1, _team2) == TeamState.Allies;
    }

    public bool CheckEnemies(Team _team1, Team _team2)
    {
        return GetState(_team1, _team2) == TeamState.Enemies;
    }

}

