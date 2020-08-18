using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour, IAttack
{
    public Vector3 attackDirection;
    public bool isAttack = false;

    public float timeAttack;
    private float durationAttack = 0.5f;
    protected float attckDistance = 2f;
    private readonly int hashAttack = Animator.StringToHash("Attack");
    [SerializeField]
    private Unit owner;

    public AttackType AttackType { get; private set; }
    public Health AttackTarget { get; private set; }

    private void Awake()
    {
        owner = GetComponent<Unit>();
        AttackType = GetComponent<AttackType>();
    }


    private void FixedUpdate()
    {
        AttackType.DoType();

        if (AttackTarget)
        {
            timeAttack -= Time.fixedDeltaTime;
            if (timeAttack < 0)
            {
                attackDirection = AttackTarget.transform.position - owner.transform.position;
                attackDirection.y = 0;
                if (attackDirection.magnitude < attckDistance)
                {
                    EventAttack();
                }
            }
        }
        else
        {
            AttackTarget = owner.FindTarget();
            isAttack = false;
        }
    }

    public void NoAttack()
    {
       // Target = null;
        isAttack = false;
    }

    public void Attack(Health newTarget)
    {
        AttackTarget = newTarget;
        isAttack = true;
    }

    public void EventAttack()
    {
        if (AttackTarget)
        {
            AttackType.Attack(AttackTarget);
            timeAttack = durationAttack;
        }
    }

    private void Attacking()
    {
        
        EventAttack();
    }

}
