using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterCommand
{
    protected Unit self { get; set; }
    protected Health Target { get; set; }
    public virtual void DoCommand()
    {
        
    }

    public virtual bool CheckCanCancel()
    {
        return false;
    }
}

public class AttackCommand : CharacterCommand
{
    private float attackRadius = 2f;
    public AttackCommand(Unit _self, Unit _target)
    {
        self = _self;
        Target = _target.health;
        DoCommand();
    }
    public override void DoCommand()
    {
        if ( (Target.transform.position - self.transform.position).magnitude < attackRadius)
        {

            self.Attack(Target);
        }
        else
        {
            self.MoveTo(Target.transform);
        }
    }

    public override bool CheckCanCancel()
    {
        if (Target == null) return true;
        return false;
    }
}
public class MoveCommand : CharacterCommand
{
    public List<Vector3> path { get; set; }
    private float permissibleLenght = 0.2f;
    public MoveCommand(Unit _self, List<Vector3> _path)
    {
        self = _self;
        path = _path;
        DoCommand();
    }
    public MoveCommand(Unit _self, Vector3 point)
    {
        path = new List<Vector3>
        {
            point
        };
        self = _self;
        DoCommand();
    }

    public override void DoCommand()
    {
        if(self.Armament.isAttack) self.NoAttack();
        self.MoveTo(path[0]);

    }
    public override bool CheckCanCancel()
    {
        CheckPath();
        //float lenght = UnityExtension.SumPath(path);
        if (path.Count == 0) return true;
        return false;
    }

    private void CheckPath()
    {
        if (path.Count == 0) return;
        Vector3 direction = (path[0] - self.transform.position);
        direction.y = 0;
        if (direction.magnitude < permissibleLenght)
        {
            path.RemoveAt(0);
            CheckPath();
        }
    }
}
public class StopCommand : CharacterCommand
{
    public Vector3 NewPosition { get; set; }
    public Vector3 GroupOffset { get; set; }
    public StopCommand(Unit _self)
    {
        self = _self;
        DoCommand();
    }

    public override void DoCommand()
    {
        self.Stop();
        self.NoAttack();
    }

}

public class PursueCommand : CharacterCommand
{
    public PursueCommand(Unit _self, Unit _target)
    {
        self = _self;
        Target = _target.health;

        DoCommand();
    }

    public override void DoCommand()
    {

    }
}
public class GuardCommand : CharacterCommand
{
    private float changeTime;
    private float maxChangeTime = 0.5f;
    public GuardCommand(Unit _self)
    {
        self = _self;
        DoCommand();
    }

    public override void DoCommand()
    {
        changeTime += Time.deltaTime;


        if (changeTime > maxChangeTime)
        {
            changeTime = 0;
            
        }
        if (Target != null)
        {
            self.Attack(Target);
        }
        else
        {
            self.NoAttack();
        }
    }


}

public class RushCommand : CharacterCommand
{
    private List<Vector3> path;
    private float curentchangeTime;
    private float changeTime = 1f;
    private float distance;
    private float trigerDistance = 0.3f;
    public RushCommand(Unit _self, List<Vector3> _path)
    {
        curentchangeTime = UnityEngine.Random.Range(0, changeTime);
        path = _path;
        self = _self;
        DoCommand();
    }
    public RushCommand(Unit _self, Vector3 point)
    {
        curentchangeTime = UnityEngine.Random.Range(0, changeTime);
        path = new List<Vector3>
        {
            point
        };
        self = _self;
        DoCommand();
    }

    public override void DoCommand()
    {
        float minDist = 0.1f;
        if (path.Count < 1) return;
         curentchangeTime += Time.deltaTime;
        if (curentchangeTime > changeTime)
        {
            curentchangeTime = 0;
            Target = self.FindTarget();
        }
        if (self.Armament.isAttack)
        {
            self.Attack(Target);
        }
        else
        {
            self.MoveTo(path[0]);
            Vector3 direct = (self.transform.position - path[0]);
            direct.y = 0;

            if(direct.magnitude < minDist)
            {
                path.RemoveAt(0);
            }
        }
    }


    //public override bool CheckCanCancel()
    //{
    //    CheckPath();
    //    if (distance == 0) return true;
    //    if(distance < 4f)
    //    {
    //        RaycastHit[] hits = Physics.RaycastAll(self.transform.position, path[0] - self.transform.position, 100.0F);
    //        Collider[] lineUnits = new Collider[hits.Length];
    //        for (int i = 0; i < hits.Length; i++)
    //        {
    //            lineUnits[i] = hits[i].collider;
    //        }
    //        List<Health> allies = self.CheckAllies(lineUnits);
    //        float maxDistance = allies.Count * 1f;
    //        if (distance < maxDistance) return true;
    //    }
    //    return false;
    //}

    //private void CheckPath()
    //{
    //    distance = 0;
    //    if (path.Count == 0) return;
    //    Vector3 direction = (path[0] - self.transform.position);
    //    direction.y = direction.y > 0 ? direction.y : 0;

    //    distance = direction.magnitude;
    //    if (distance < trigerDistance)
    //    {
    //        path.RemoveAt(0);
    //        CheckPath();
    //    }
    //}

}


public class FindEnemyCommand : CharacterCommand
{
    private float currentTime;
    private float findTime = 0.3f;

    public FindEnemyCommand(Unit _self)
    {
        currentTime = UnityEngine.Random.Range(0, findTime);
        self = _self;
        DoCommand();
    }

    public override void DoCommand()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = findTime;
            List<Health> enemies = self.FilterEnemy(Physics.OverlapSphere(self.transform.position, 40f));
            Target = self.GetClosest(enemies) as Health;
        }
        if(Target != null)
        {
            if ((Target.transform.position - self.transform.position).magnitude < self.targetRadius)
                self.Attack(Target);
            else self.MoveTo(Target.transform);
        }
    }

    public override bool CheckCanCancel()
    {
        return false;
    }
}