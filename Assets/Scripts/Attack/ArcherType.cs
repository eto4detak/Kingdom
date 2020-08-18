using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherType : AttackType
{
    public Arrow arrowPrefab;
    public float velocity = 2f;
    public Transform firePoint;

    private void Awake()
    {
        type = UnitType.archer;
    }

    public override void DoType()
    {
        //Health enemy = UnitsManager.instance.GetClosestEnemy(unitOwner.health);
        //unitOwner.Attack(enemy);
    }

    public override void Attack(Health enemy)
    {
        if (enemy != null && owner != null) LookAtTarget(enemy, owner);
        if (owner == null) return;
        float distance = (owner.transform.position - enemy.transform.position).magnitude;
        float deltaOffset = distance / 20;
        var arraow = GameObject.Instantiate(arrowPrefab, firePoint.transform.position, firePoint.transform.rotation);
        arraow.Setup(damage + bonusDamage, owner.GetTeam());
        arraow.GetComponent<Renderer>().material = UnitsManager.instance.GetTeamColor(owner.GetTeam());
        Vector3 offset = new Vector3(Random.Range(-deltaOffset, deltaOffset), 
            Random.Range(0, deltaOffset), Random.Range(-deltaOffset, deltaOffset));

        Vector3 gravitiDistance = Vector3.up * distance * distance /100;

        arraow.transform.LookAt(enemy.GetCenter().position + offset + gravitiDistance);
        arraow.rb.AddForce(arraow.transform.forward * velocity, ForceMode.Impulse);
    }


}

