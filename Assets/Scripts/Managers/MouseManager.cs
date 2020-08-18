using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseManager : Singleton<MouseManager>
{
    public List<Vector3> mousePath = new List<Vector3>();
    public RaycastHit mouseHit;

    private Vector2 startPos;
    private Vector2 endPos;
    private float timeOldClick;
    private float timeCheckDoubleClick = 0.3f;
    private float findRadius = 1.5f;
    private float minY = 0;
    private int characterLayer;
    private int pathLayer;
    private int noPathLayer;
    private bool dragMouse = false;
    private bool isDoubleClick = false;


    private void Start()
    {
        characterLayer = LayerMask.GetMask("Character");
        pathLayer = LayerMask.GetMask("Path") & ~LayerMask.GetMask("Character");
        noPathLayer = LayerMask.GetMask("NoPath");
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DownMouse();
        }

        if (Input.GetMouseButtonUp(0))
        {
            UpMouse();
        }

        if (dragMouse)
        {
            Drag();
        }
    }

    private void DownMouse()
    {
        dragMouse = true;
        mousePath = new List<Vector3>();
        DoMouseHit(characterLayer);
        Selected.instance.TrySelectUnits();

    }

    private void UpMouse()
    {
        if (dragMouse)
        {
           Selected.instance.CreateAttackCommand();
        }
        dragMouse = false;
        TrailManager.instance.SpeedUpCurrentTrail();
    }

    private void Drag()
    {
        DoMouseHit(noPathLayer);
        if (mouseHit.collider == null)
        {
            DoMouseHit(pathLayer);
            if (Tumbler.instance.state == TublerState.path)
            {
                TrailManager.instance.StepTrail(mouseHit);
            }
            mousePath.Add(mouseHit.point);
        }
    }

    public bool IsDoubleClick()
    {
        if (Time.time < timeOldClick + timeCheckDoubleClick)
        {
            return true;
        }
        else
        {
            timeOldClick = Time.time;
            return false;
        }
    }

    public void Clear()
    {
        mousePath.Clear();
    }



    public void DoMouseHit(int layout)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(mouseRay, out mouseHit, 100, layout);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData e = new PointerEventData(EventSystem.current);
        e.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(e, results);
        return results.Count > 0;
    }
    
    private void CheckLeftClick()
    {
        if (IsPointerOverUIObject()) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Unit unitTarget;
            
            if (Physics.Raycast(mouseRay, out RaycastHit mouseHit, 100, characterLayer))
            {
                //attack
                unitTarget = mouseHit.collider.GetComponent<Unit>();
                if (unitTarget)
                {
                    OnClickRightObject(unitTarget, mouseHit.point);
                    return;
                }

                //move
                Collider filterTerrain = mouseHit.collider.GetComponent(typeof(Collider)) as Collider;
                if (filterTerrain)
                {
                    OnClickRight(mouseHit.point);
                    return;
                }
            }
        }
    }



    private void OnClickRightObject(Unit target, Vector3 hitPoint)
    {
        Unit selected = UnitsManager.instance.GetClosestFreePlayerUnit(hitPoint);
        if (selected != null && target.GetTeam() != selected.GetTeam())
            selected.Command = new AttackCommand(selected, target);
    }

    private void OnClickRight(Vector3 hitPoint)
    {
        Unit selected = UnitsManager.instance.GetClosestFreePlayerUnit(hitPoint);
        if(selected != null)
        selected.Command = new RushCommand(selected, hitPoint);
    }
}

