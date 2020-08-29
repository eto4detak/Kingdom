using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public Transform target;
    
    #region Singleton
    static protected MissionManager s_Instance;
    static public MissionManager instance { get { return s_Instance; } }
    #endregion

    void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion

    }


    public void SetStartingPlayerTarget(List<Unit> freeUnits)
    {
        for (int i = 0; i < freeUnits.Count; i++)
        {
            freeUnits[i].Command = new GuardCommand(freeUnits[i]);
        }
    }
    public void SetStartingTarget(List<Unit> units)
    {
        for (int i = 0; i < units.Count; i++)
        {
            Unit enemy = UnityExtension.GetClosest(units[i].transform, UnitsManager.instance.localities)
                as Unit;
            units[i].Command = new GuardCommand(units[i]);
        }
    }
}
