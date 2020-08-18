using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VictoryPoint : MonoBehaviour
{
    public Renderer meshBody;
    public Material captureMaterial;
    public UnityEvent Capture = new UnityEvent();
    public Unit attached;
    public bool isCapture;

    private void OnTriggerStay(Collider other)
    {
        Unit playerUnit = CheckPlayerUnit(other);
        if (!isCapture && playerUnit != null)
        {
            isCapture = true;
            SwitchToTeam(playerUnit.health);
            ChangeColor();
            Capture?.Invoke();
        }
    }

    private Unit CheckPlayerUnit(Collider other)
    {
        Unit ch = other.GetComponent<Unit>();
        if (ch != null && ch.GetTeam() == Team.Player1)
        {
            return ch;
        }
        return null;
    }


    public void ChangeColor()
    {
        meshBody.material = captureMaterial;
    }

    private void SwitchToTeam(Health newOwner)
    {
        if (attached == null) return;
        attached.health.SetTeam(newOwner.GetTeam());
      //  attached.command = new GuardCommand(attached);
    }

}
