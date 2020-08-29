using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMode : Singleton<GMode>
{
    private bool isPause;
    private bool isMute;

    protected override void Awake()
    {
        base.Awake();
        isMute = SaveLoad.GetInstance().pData.musicMute;
    }

    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0;
        MusicPlayer.instance.StopFirstSound();
    }

    public void ContinueGame()
    {
        isPause = false;
        Time.timeScale = 1;
        if (!SaveLoad.GetInstance().pData.musicMute)
        {
            MusicPlayer.instance.StopFirstSound();
            MusicPlayer.instance.PlayFirstSound();
        }
    }

    public void Mute(bool mute)
    {
        isMute = mute;
        MusicPlayer.instance.StopFirstSound();
        SaveLoad.GetInstance().pData.musicMute = isMute;
        SaveLoad.GetInstance().Save();
    }
}
