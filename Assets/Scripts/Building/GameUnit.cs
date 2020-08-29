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
    public Team team;
    public Culture culture;
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
        culture.changed.AddListener(ChangeCulture);
    }

    public void ChangeCulture()
    {
        List<Locality> cites = new List<Locality>(UnitsManager.instance.localities);
        cites.Remove((Locality) this);
        cites.RemoveAll(x => x.culture.cults[0].name != culture.cults[0].name);
        Locality closest = transform.GetClosestT(cites);
        if (closest)
        {
            team = closest.team;
        }
        else
        {

            Debug.Log("error closest " + cites.Count);

        }
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
