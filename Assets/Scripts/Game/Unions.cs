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

        public void SetUnion(TeamState state)
        {
            Union = state;
        }
    }

    public List<p_unions> _Unions = new List<p_unions>();
    private TeamState notFind = TeamState.Neitrals;

    public TeamState GetState(Team _team1, Team _team2)
    {
        if (_team1 == _team2) return TeamState.Self;
        int index = FindUnionIndex(_team1, _team2);
        if (index < 0)
        {
            return notFind;
        }
        else
        {
            return _Unions[index].Union;
        }
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


    public void SetUnion(Team _team1, Team _team2, TeamState uState)
    {
        if (_team1 == _team2) return;
        int index = FindUnionIndex(_team1, _team2);
        if (index < 0)
        {
            _Unions.Add(new p_unions()
            {
                Name = _team1.ToString() + _team2.ToString(),
                Team1 = _team1,
                Team2 = _team2,
                Union = uState,
            });
        }
        else
        {
            _Unions[index].SetUnion(uState);
        }

    }

    private int FindUnionIndex(Team _team1, Team _team2)
    {
        int index = -1;
        for (int i = 0; i < _Unions.Count; i++)
        {
            if ((_Unions[i].Team1.Equals(_team1) && _Unions[i].Team2.Equals(_team2))
                || (_Unions[i].Team1.Equals(_team2) && _Unions[i].Team2.Equals(_team1)))
            {
                return i;
            }
        }
        return index;
    }
}

