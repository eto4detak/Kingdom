using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Locality
{
    protected override void Awake()
    {
        base.Awake();
        power.SetPower(20);
        int startpeople = 40;
        int neitralcount = 10;
        if (team != Team.Neitral)
        {
            for (int i = 0; i < startpeople; i++)
            {
                people.AddHuman(new Human() { team = team });
            }
        }
        for (int i = 0; i < neitralcount; i++)
        {
            people.AddHuman(new Human() { team = Team.Neitral });
        }

        AddPotential(new GameUnitInfo { name = "Immigrants", start = CreateImmigrants });
        AddPotential(new GameUnitInfo { name = "General", start = CreateGeneral });
    }

    protected override void Start()
    {
        base.Start();
        ChangeLocality();
        LevelManager.instance.playLevel.AddListener(ChangeLocality);
        GameTime.instance.timeChanged.AddListener(ChangeLocality);
        food.hunger += Hunger;
    }


    public void Hunger(int foodLack)
    {
        float coeff = 0.1f;
        int kills =(int)Mathf.Ceil(foodLack * coeff);
        people.KillHumans(kills);
    }


    public void CreateImmigrants()
    {
        int people = 20;
        var immigrants = CreateFormation<Immigrants>(people, team);
        if (immigrants)
        {
            immigrants.Setup(this);
            immigrants.Select();
        }
    }

    public void CreateGeneral()
    {
        int people = 20;
        var unit = CreateFormation<General>(people, team);

        if (unit)
        {
            unit.Setup(this);
            unit.Select();
        }
    }

    public override void Lose(GameUnit winner)
    {
        ChangeTeam(winner.team);
        JoinUnit(winner);
    }



    public override void ChangeState(UnitState newState)
    {
        if (state == UnitState.Blockade && newState != UnitState.LiftBlockade) return;
        if (state == newState) return;
        if (stateLoop != null)
        {
            StopCoroutine(stateLoop);
        }
        state = newState;

        switch (state)
        {
            case UnitState.Development:
                stateLoop = LoopDevelopment();
                break;
            case UnitState.Absort:
                stateLoop = LoopAbsorb();
                break;
            case UnitState.Blockade:
                stateLoop = LoopBlock();
                break;
            case UnitState.LiftBlockade:
                ChangeLocality();
                break;
            default:
                break;
        }

        if (stateLoop != null)
        {
            StartCoroutine(stateLoop);
        }
    }

    protected override void ChangeLocality()
    {
        GameTime time = GameTime.instance;

        if(time.seaason == Period.Winter)
        {
            ChangeState(UnitState.Absort);
            return;
        }

        switch (time.timeDay)
        {
            case TimeDay.Day:
                ChangeState(UnitState.Development);
                break;
            case TimeDay.Night:
                ChangeState(UnitState.Absort);
                break;
            default:
                break;
        }
    }

    protected IEnumerator LoopAbsorb()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Absorb();
        }
    }

    protected IEnumerator LoopDevelopment()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Development();
        }
    }
    protected IEnumerator LoopBlock()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Absorb();
        }
    }

    protected virtual void Absorb()
    {
        float foodPercent = 0.1f;
        int addFood = -(int)(people.humans.Count * foodPercent);
        food.Change(addFood);
    }

    protected virtual void Development()
    {
        int dev = 1;
        float foodPercent = 0.1f;
        int addFood = (int)(people.humans.Count * foodPercent);

        food.Change(addFood);
        people.Development(dev);
    }

}


