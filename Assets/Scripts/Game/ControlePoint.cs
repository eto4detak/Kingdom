using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePoint : MonoBehaviour
{
    public Team team;
    private int characterLayer;
    private void Start()
    {
        characterLayer = LayerMask.NameToLayer("Character");
    }

    private void OnTriggerStay(Collider other)
    {
        if( other.gameObject.layer == characterLayer)
        {
            TryDestroy(other);
        }
    }

    public void TryDestroy(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if(unit.GetTeam() != team)
        {
            Destroy(gameObject);
        }
    }

}
