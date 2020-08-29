using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, IMovable, IAttack, IUnit
{
    public float bonusColor = 50f;
    public float targetRadius = 1.5f;
    public bool isFree = true;
    public Health health;
    public CharacterEvent die = new CharacterEvent();
    public CharacterEvent EventStart = new CharacterEvent();
    public CharacterContrMovement movement;
    [HideInInspector] public List<CharacterCommand> commands = new List<CharacterCommand>();
    [SerializeField] private ParticleSystem psSelected;
    private UnitColor unitColor;
    private Characteristic characteristic;

    public CharacterAttack Armament { get; private set; }
    public CharacterCommand Command { get; set; }

    protected void Awake()
    {
        Armament = GetComponent<CharacterAttack>();
        movement = GetComponent<CharacterContrMovement>();
        unitColor = GetComponent<UnitColor>();
        List<IBonus> sourse = new List<IBonus>(GetComponents<IBonus>());
        characteristic = new Characteristic(sourse);
    }

    protected void Start()
    {
        EventStart.Invoke(this);
        health?.EventDie.AddListener(OnDie);
        Command = new GuardCommand(this);
    }

    protected void FixedUpdate()
    {
        if (Command != null)
        {
            Command.DoCommand();
        }
    }

    public void AddBonus(IBonus bonus)
    {
        characteristic.AddBonus(bonus);
        unitColor.StartFlicker();
    }

    public void RemoveBonus(IBonus bonus)
    {
        characteristic.RemoveBonus(bonus);
        unitColor.NoFlicker();
    }

    public void ChangeUnitActivity(bool active)
    {
        enabled = active;
        movement.enabled = active;
        Armament.enabled = active;
    }


    public void PushCommand(CharacterCommand _command)
    {
        List < CharacterCommand > newCommands = new List<CharacterCommand>();
        newCommands.Add(_command);

        if (commands.Count > 0)
            newCommands.Add(commands[commands.Count - 1]);
        else newCommands.Add(Command);
        commands = newCommands;
        Command = _command;
    }


    public void SetPathCommand(List<Vector3> path)
    {
        if (Armament.AttackType is ArcherType)
        {
            Command = new MoveCommand(this, path);
        }
        else
        {
            Command = new RushCommand(this, path);
        }
    }

    public void MoveTo(Transform target)
    {
        isFree = false;
        Armament.NoAttack();
        movement.MoveTo(target);
    }
    public void MoveTo(Transform target, Vector3 lookToPoint)
    {
        isFree = false;
        Armament.NoAttack();
        movement.MoveTo(target);

    }

    public void MoveTo(Vector3 target)
    {
        isFree = false;
        Armament.NoAttack();
        movement.MoveTo(target);
    }

    public void SetSpeed(float _speed)
    {
        movement.SetSpeed(_speed);
    }

    public void Attack(Health newTarget)
    {
        isFree = false;
        if(movement != null) movement.Stop();
        Armament.Attack(newTarget);
    }

    public void NoAttack()
    {
        isFree = true;
        Armament.NoAttack();
    }
    public void Stop()
    {
        isFree = true;
        if(movement != null) movement.Stop();
    }

    public List<Health> FilterEnemy(Collider[] findStack)
    {
        List<Health> found = new List<Health>();
        Health tempUnit;
        for (int i = 0; i < findStack.Length; i++)
        {
            tempUnit = findStack[i].GetComponent<Health>();
            if (tempUnit == null || !Unions.instance.CheckEnemies(tempUnit.GetTeam(), health.GetTeam()) ) continue;
            found.Add(tempUnit);
        }
        return found;
    }

    public List<Health> CheckAllies(Collider[] findStack)
    {
        List<Health> found = new List<Health>();
        Health tempUnit;
        for (int i = 0; i < findStack.Length; i++)
        {
            tempUnit = findStack[i].GetComponent<Health>();
            if(tempUnit && Unions.instance.CheckAllies(tempUnit.GetTeam(), health.GetTeam())) found.Add(tempUnit);
        }
        return found;
    }

    public Team GetTeam()
    {
        return health.GetTeam();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Health FindTarget(float radius = 0)
    {
        return UnitsManager.instance.GetClosestEnemy(health);
    }

    public List<Unit> GetClosestGroup(List<Unit> stack, float groupRadius = 7f)
    {
        List<Unit> group = new List<Unit> { this };
        Vector3 dirToTarget;
        float sqR = groupRadius * groupRadius;
        for (int i = 0; i < stack.Count; i++)
        {
            if (stack[i] == null) continue;
            dirToTarget = stack[i].transform.position - transform.position;
            bool sameType =  stack[i].Armament.AttackType.GetType() == Armament.AttackType.GetType();
            if (dirToTarget.sqrMagnitude < sqR && sameType)
            {
                group.Add(stack[i]);
            }
        }

        return group;
    }

    public void PlaySelected()
    {
        if (psSelected == null) return;
        psSelected.gameObject.SetActive(true);
        psSelected.Play();
    }

    protected void OnDie()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<CharacterContrMovement>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Armament.enabled = false;
        enabled = false;
        die?.Invoke(this);
        FormationsManager.instance.DieUnit(this);
        Destroy(gameObject);
    }
    protected void UpdateCharacteristic()
    {
        Armament.AttackType.bonusDamage = characteristic.GetBonus(TypeBonus.Attack).value;
        health.bonusArmor = characteristic.GetBonus(TypeBonus.Armor).value;
    }

}

[System.Serializable]
public class CharacterEvent : UnityEvent<Unit>
{
}