using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnitState
{
    Stop,
    Move,
    Back,
    Attack
}

public class GameUnitInfo
{
    public string name;
    public int goldCost;
    public int peopleCost;
    public UnityAction start;
}

public class GameUnit : MonoBehaviour, ISelected
{
    //legitimacy
    //popularity
    //respect
    public Team team;
    public Culture culture;
    public Loyalty loyalty = new Loyalty();
    public List<GameUnitInfo> availablePotential = new List<GameUnitInfo>();
    public UnityEvent changed = new UnityEvent();

    [SerializeField] private string myName;
    [SerializeField] private int people;
    [SerializeField] private int gold;
    public string MyName { get => myName; set => myName = value; }
    public int People
    {
        get => people;
        set
        {
            people = value;
            changed.Invoke();
        }
    }
    public int Gold
    {
        get => gold;
        set
        {
            gold = value;
            changed.Invoke();
        }
    }

    protected virtual void Awake()
    {
    }

    public void Select()
    {
        SelectedPanel.instance.TrySelect(this, Team.Player1);
    }

    public void AddPotential(GameUnitInfo potencial)
    {
        availablePotential.Add(potencial);
    }



}
