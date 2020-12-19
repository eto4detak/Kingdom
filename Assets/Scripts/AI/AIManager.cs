using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : Singleton<AIManager>
{
    private int superiority;
    private int cavaleryPlayerCount;
    private int meleePlayerCount;
    private int archerPlayerCount;
    private int meleeEnemyCount;
    private int archerEnemyCount;
    private int cavaleryEnemyCount;

    private IAITactics currentTactics;
    private List<IAITactics> tactics = new List<IAITactics>();

    private void Start()
    {
        AddTactics();
    }


    public void AddTactics()
    {
        tactics.Add(new AttackTactics());
        tactics.Add(new EvasionTactics());
        tactics.Add(new SpellEvasionTactics());
        tactics.Add(new LittleManaTactics());
        tactics.Add(new DominanceTactics());
    }


    private void FixedUpdate()
    {
        currentTactics = FindOptimalTactics();

        if(currentTactics != null)
        {
            currentTactics.Control();
        }
    }


    private IAITactics FindOptimalTactics()
    {
        float need = -1;
        float currentNeed;
        IAITactics result = null;
        for (int i = 0; i < tactics.Count; i++)
        {
            currentNeed = tactics[i].CheckNeed();
            if (currentNeed > need)
            {
                need = currentNeed;
                result = tactics[i];
            }
        }
        return result;
    }

    public void Statistic()
    {
        //meleePlayerCount = UnitsManager.instance.GetPlayerUnit(UnitType.melee).Count;
        //archerPlayerCount = UnitsManager.instance.GetPlayerUnit(UnitType.archer).Count;
        //cavaleryPlayerCount = UnitsManager.instance.GetPlayerUnit(UnitType.cavalery).Count;
        //meleeEnemyCount = UnitsManager.instance.GetEnemyUnit(UnitType.melee).Count;
        //archerEnemyCount = UnitsManager.instance.GetEnemyUnit(UnitType.archer).Count;
        //cavaleryEnemyCount = UnitsManager.instance.GetEnemyUnit(UnitType.cavalery).Count;

        if (cavaleryEnemyCount > meleeEnemyCount)
        {
            if (UnitsManager.instance.localities.Count != 0)
            {
                //foreach (var enemy in UnitsManager.instance.enemyUnits)
                //{
                //    enemy.Command = new AttackCommand(enemy, UnitsManager.instance.builds[0]);
                //}
            }

        }
    }
}


public enum AITactics
{
    waiting,
    attack,
    defense
}