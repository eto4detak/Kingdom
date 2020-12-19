using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Unit owner;
    void Start()
    {
        owner = GetComponent<Unit>();
        owner.Command = new GuardCommand(owner);
    }

}
