using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnitState
{
    Stop,
    Move,
    Back,
    Attack,
    Blockade,
    LiftBlockade,
    Absort,
    Development,
}

public class GameUnitInfo
{
    public string name;
    public int goldCost;
    public int peopleCost;
    public UnityAction start;
}

[Serializable]
public class GameUnit : MonoBehaviour, ISelected
{
    public Team team;
    public Loyalty loyalty = new Loyalty();
    public List<GameUnitInfo> availablePotential = new List<GameUnitInfo>();
    [HideInInspector]
    public UnityEvent unitChanged = new UnityEvent();
    public People people = new People();
    public Food food = new Food();
    public UnitPower power = new UnitPower();
    public UnitState state;
    protected IEnumerator stateLoop;

    [SerializeField] private string sName;
    [SerializeField] private int gold;
    public string SName { get => sName; set => sName = value; }

    public int Gold
    {
        get => gold;
        set
        {
            gold = value;
            unitChanged.Invoke();
        }
    }

    protected virtual void Awake()
    {
        unitChanged.AddListener(UpdateCharacters);
        people.peopleChanged.AddListener(unitChanged.Invoke);
        food.foodChanged.AddListener(unitChanged.Invoke);
    }


    protected virtual void Start()
    {
        GameTime.instance.timeChanged.AddListener(ChangeLocality);
    }

    public virtual void JoinUnit(GameUnit traveler)
    {
        people.Join(traveler.people);
        Gold += traveler.Gold;

        Destroy(traveler.gameObject);
    }

    public bool CanJoin(GameUnit joined)
    {
        return gameObject != joined.gameObject
            && !Unions.instance.CheckEnemies(joined.team, team);
    }

    protected virtual void ChangeLocality()
    {

    }

    public void Select()
    {
        SelectedPanel.instance.ViewUnit(this, Team.Player1);
    }

    public void AddPotential(GameUnitInfo potencial)
    {
        availablePotential.Add(potencial);
    }

    private void UpdateCharacters()
    {
        team = people.FindMainTeam();
    }

    public virtual bool CanBattle()
    {
        int minBattle = 1;

        return power.GetPower() > minBattle;
    }

    public virtual void Lose(GameUnit winner)
    {
        Destroy(gameObject);
    }

    public virtual void ChangeState(UnitState newState)
    {
        if (state == newState) return;
        if (stateLoop != null)
        {
            StopCoroutine(stateLoop);
        }
        state = newState;

        switch (state)
        {
            default:
                break;
        }

        if (stateLoop != null)
        {
            StartCoroutine(stateLoop);
        }
    }
}