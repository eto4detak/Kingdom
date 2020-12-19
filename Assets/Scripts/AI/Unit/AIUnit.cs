using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUnit : MonoBehaviour
{
    protected Unit owner;
    protected bool isManeuver;
    protected float timeManeuver = 1f;
    protected float currentTimeManeuver;


    protected void Awake()
    {
        owner = GetComponent<Unit>();
        owner.health.EventDie.AddListener(OnDestroy);
    }

    protected void OnDestroy()
    {
        Destroy(this);
    }

    private void Start()
    {
            
    }

    private void Update()
    {
        if (owner == null || owner.enabled == false) enabled = false;
    }


    public virtual void StartCommand()
    {
        
    }


    protected void Maneuver()
    {
        isManeuver = true;
      //  owner.command = new MoveCommand(owner, transform.position + transform.right * 2);
    }
    protected void StopManeuver()
    {
        isManeuver = false;

    }
}
