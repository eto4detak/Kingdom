using System;
using System.Collections.Generic;
using UnityEngine;

public class GManager : Singleton<GManager>
{
    public static float deltaPositionY = 0.1f;
    public static float minPositionY = -0.1f;
    public Team selfTeam;

    void Start()
    {
        LevelManager.instance.ChangeLevelState(LevelManager.LevelState.LoadLevel);
    }

}