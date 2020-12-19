using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public GameUnit participant1;
    public GameUnit participant2;

    public void Registration(GameUnit self, GameUnit target)
    {
        if(self == target)
        {
            throw new System.Exception("error new Battle");
        }
        participant1 = self;
        participant2 = target;
        StartCoroutine(StartBattle());
    }

    public IEnumerator StartBattle()
    {
        float delay = 1f;
        while (true)
        {
            if (!participant1.CanBattle())
            {
               StartCoroutine( Victory(participant2, participant1));
                yield break;
            }
            if (!participant2.CanBattle())
            {
               StartCoroutine( Victory(participant1, participant2));
                yield break;
            }
            yield return new WaitForSeconds(delay);
            Skirmish();
        }
    }

    public IEnumerator Victory(GameUnit winner, GameUnit loser)
    {
        loser.Lose(winner);
        yield break;
    }

    public void Skirmish()
    {
        float percentForce = 0.1f;
        float hit1 = participant1.power.GetPower() * percentForce;
        float hit2 = participant2.power.GetPower() * percentForce;
        participant1.power.ChangePower( -(int)hit2);
        participant2.power.ChangePower( -(int)hit1);
    }

}
