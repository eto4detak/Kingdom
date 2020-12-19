using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Immigrants : GameUnit
{
    public CharacterContrMovement movement;
    public GameUnit home;

    private Vector3? moveTarget = null;
    protected override void Awake()
    {
        base.Awake();
        AddPotential(new GameUnitInfo { name = "Move to", start = MoveToOrHome });
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(FindHome());
    }

    protected void OnTriggerEnter(Collider other)
    {
        Locality newHome = other.GetComponent<Locality>();
        if (home == newHome && state != UnitState.Back) return;
        if (newHome != null && newHome.CanJoin(this))
        {
            newHome.JoinUnit(this);
        }
    }
    public IEnumerator FindHome()
    {
        float minDist = 0.1f;

        while (true)
        {
            if (moveTarget != null)
            {
                Vector3 vDist = moveTarget.Value - transform.position;
                vDist.y = 0;
                float dist = vDist.magnitude;

                if (dist < minDist)
                {
                    BackToHome();
                    yield break;
                }
            }
            yield return null;
        }
    }

    public void Setup(GameUnit oldhome)
    {
        home = oldhome;
    }


    public void SetTarget(Vector3? target)
    {
        moveTarget = target;
        MoveToOrHome();
    }

    public void MoveToOrHome()
    {
        if (moveTarget == null)
        {
            TryFindTarget();
        }
        else
        {
            movement.MoveTo(moveTarget.Value);
        }
    }

    protected void TryFindTarget()
    {
        MouseManager.instance.ChangeFind(FindTarget.Point, SetTarget);
    }

    protected void BackToHome()
    {
        state = UnitState.Back;
        movement.MoveTo(home.transform);
    }

}


