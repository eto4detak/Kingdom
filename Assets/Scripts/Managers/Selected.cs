using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : Singleton<Selected>
{
    public int characterLayer;

    private List<Unit> selected = new List<Unit>();

    public void Start()
    {
        characterLayer = LayerMask.GetMask("Character");
    }





    public void AddUnit(Unit added)
    {
        Formation form = FormationsManager.instance.FindFormation(added);

        if (form != null)
        {
            for (int i = 0; i < form.party.Count; i++)
            {
                TryAddedUnit(form.party[i]);
            }
            return;
        }
        else
        {
            TryAddedUnit(added);
        }
    }

    public void AddUnits(List<Unit> units)
    {
        for (int i = 0; i < units.Count; i++)
        {
            AddUnit(units[i]);
        }
    }
    public void TryFindSelect()
    {
        TrySelect();

        if (selected.Count > 0)
        {
            TrailManager.instance.CreateTrail(MouseManager.instance.mouseHit);
        }
    }

    public void TrySelectUnits()
    {
        if (Tumbler.instance.state == TublerState.path)
        {
            TrySelect();

            if (selected.Count > 0)
            {
                TrailManager.instance.CreateTrail(MouseManager.instance.mouseHit);
            }
        }
    }

    public void CreateMoveCommand()
    {
        if (selected.Count == 0) return;
        Vector3 startPoint = selected[0].transform.position;
        ImprovePath();
        Vector3 offset;
        for (int i = 0; i < selected.Count; i++)
        {
            offset = selected[i].transform.position - startPoint;
            List<Vector3> gPath = new List<Vector3>(MouseManager.instance.mousePath);
            for (int g = 0; g < gPath.Count; g++)
            {
                gPath[g] += offset;
            }
            selected[i].SetPathCommand(gPath);
        }
        selected.Clear();
    }
    public void CreateAttackCommand()
    {
        //if (selected.Count == 0) return;
        //Vector3 startPoint = selected[0].transform.position;
        //List<Unit> party = UnitsManager.instance.GetClosestUnits(startPoint);
        //ImprovePath();
        //Vector3 offset;
        //for (int i = 0; i < party.Count; i++)
        //{
        //    offset = party[i].transform.position - startPoint;
        //    List<Vector3> gPath = new List<Vector3>(MouseManager.instance.mousePath);
        //    for (int g = 0; g < gPath.Count; g++)
        //    {
        //        gPath[g] += offset;
        //    }
        //    party[i].SetPathCommand(gPath);
        //}
        //selected.Clear();
    }

    public void TrySelect()
    {
        Unit tempSelected = null;
        selected.Clear();

        if (MouseManager.instance.mouseHit.collider != null)
        {
            tempSelected = MouseManager.instance.mouseHit.collider.GetComponent<Unit>();
        }

        if (tempSelected == null || tempSelected.GetTeam() != Team.Player1) return;
        MouseManager.instance.Clear();

        AddUnit(tempSelected);
    }

    private void ImprovePath()
    {
        int indexRemove = -1;
        int maxWrongPath = 30;
        Vector3 direction = Vector3.zero;

        for (int i = 0; i < maxWrongPath && i < MouseManager.instance.mousePath.Count; i++)
        {
            direction = (MouseManager.instance.mousePath[i] - selected[0].transform.position);
            direction.y = direction.y > 0 ? direction.y : 0;
            if (direction.magnitude < 0.5f)
            {
                indexRemove = i;
            }
        }
        if (indexRemove > -1)
        {
            MouseManager.instance.mousePath.RemoveRange(0, indexRemove);
        }
    }

    private void TryAddedUnit(Unit added)
    {
        if (selected.Exists(x => x == added)) return;
        selected.Add(added);
    }


    private void FindUnitRadius()
    {
        //float findRadius = 1.5f;

        //if (MouseManager.instance.mouseHit.collider == null)
        //{
        //    MouseManager.instance.DoMouseHit(-1);
        //    Collider[] units = Physics.OverlapSphere(MouseManager.instance.mouseHit.point, findRadius, characterLayer);
        //    if (units.Length > 0)
        //    {
        //        tempSelected = UnityExtension.GetClosest(MouseManager.instance.mouseHit.point, UnitsManager.instance.playerUnits) as Unit;
        //    }
        //    else return;
        //}
        //else
        //{
        //    tempSelected = MouseManager.instance.mouseHit.collider.GetComponent<Unit>();
        //}
    }

}
