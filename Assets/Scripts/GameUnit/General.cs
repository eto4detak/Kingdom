using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : GameUnit
{
    public CharacterContrMovement movement;
    public GameUnit home;

    private Vector3? moveTarget = null;
    protected override void Awake()
    {
        int force = 50;
        base.Awake();
        AddPotential(new GameUnitInfo { name = "Conquer", start = FindConquer });
        power.SetPower(force);
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Setup(GameUnit oldhome)
    {
        home = oldhome;
    }

    public void FindConquer()
    {
        MouseManager.instance.ChangeFind(FindTarget.Point, StartConquerLoop);
    }

    protected void StartConquerLoop(Vector3? target)
    {
        StartCoroutine(MoveAway(target));
    }

    protected IEnumerator MoveAway(Vector3? target)
    {
        float attackDist = 1f;

        if (target != null)
        {
            moveTarget = target;
            movement.MoveTo(target.Value);
        }
        while (true)
        {
            if (moveTarget != null)
            {
                Vector3 vDist = moveTarget.Value - transform.position;
                vDist.y = 0;
                if (vDist.magnitude < attackDist)
                {
                    StartCoroutine(FindConquerTarget());
                    yield break;
                }
            }
            yield return null;
        }
    }

    protected IEnumerator FindConquerTarget()
    {
        Team newTeam = UnitsManager.instance.GetNewTeam();
        Unions.instance.SetUnion(team, newTeam, TeamState.Allies);
        people.SetTeam(newTeam);
        while (true)
        {
            List<Locality> closestLocs = UnitsManager.instance.GetClosestLocalities(gameObject);

            for (int i = 0; i < closestLocs.Count; i++)
            {
                bool allies = Unions.instance.CheckAllies(team, closestLocs[i].team);
                if (!allies)
                {
                    StartCoroutine(MoveToConquer(closestLocs[i]));
                    yield break;
                }
            }
            yield return null;
            
        }
    }

    protected IEnumerator MoveToConquer(Locality target)
    {
        float minDist = 1f;

        moveTarget = target.transform.position;
        movement.MoveTo(moveTarget.Value);
        while (true)
        {
            if (moveTarget != null)
            {
                Vector3 vDist = moveTarget.Value - transform.position;
                vDist.y = 0;
                if (vDist.magnitude < minDist)
                {
                    moveTarget = null;
                    movement.Stop();
                    StartCoroutine(Conquer(target));
                    yield break;
                }
            }
            yield return null;
        }
    }

    protected IEnumerator Conquer(Locality target)
    {
        BattleManager.instance.Registration(transform.position, this, target);
        yield break;
    }

    protected void BackToHome()
    {
        state = UnitState.Back;
        movement.MoveTo(home.transform);
    }



}
