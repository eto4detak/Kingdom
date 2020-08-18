using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteUI : Singleton<GameCompleteUI>
{
    public Canvas canvas;
    public void ShowPanel()
    {
        canvas.enabled = true;
    }


    public void RestartLevel()
    {
        LevelManager.instance.RestartLevel();
    }

}
