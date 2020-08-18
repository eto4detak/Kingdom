using System;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static float deltaPositionY = 0.1f;
    public static float minPositionY = -0.1f;

    void Start()
    {
        LevelManager.instance.ChangeLevelState(LevelManager.LevelState.StartLevel);
    }


}