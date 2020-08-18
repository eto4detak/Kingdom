using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameWin 
{
    public int star;

    public GameWin(int _star = 0)
    {
        star = _star;
        star = star > 0 ? star : 0;
    }

    public void FinishLevel()
    {
        MusicPlayer.instance.PlayWinSound();
        SaveLoad.GetInstance().Load();
        SaveLoad.GetInstance().pData.star += star;
        SaveLoad.GetInstance().Save();
        SaveLoad.GetInstance().Load();
        GameHUD.instance.SetTotalStar(SaveLoad.GetInstance().pData.star);
        GameHUD.instance.btnContinue.gameObject.SetActive(true);
    }

}

