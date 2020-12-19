using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMode : Singleton<GMode>
{
    private ModeState state;
    private bool isMute;

    protected override void Awake()
    {
        base.Awake();
        isMute = SaveLoad.GetInstance().pData.musicMute;
    }

    public void PauseGame()
    {
        state = ModeState.Pause;
        Time.timeScale = 0;

        Debug.Log("PauseGame ");

        //MusicPlayer.instance.StopFirstSound();
    }

    public void ContinueGame()
    {
        Debug.Log("ContinueGame ");

        state = ModeState.Play;
        Time.timeScale = 1;
        //if (!SaveLoad.GetInstance().pData.musicMute)
        //{
        //    MusicPlayer.instance.StopFirstSound();
        //    MusicPlayer.instance.PlayFirstSound();
        //}
    }

    public void Mute(bool mute)
    {
        isMute = mute;
        MusicPlayer.instance.StopFirstSound();
        SaveLoad.GetInstance().pData.musicMute = isMute;
        SaveLoad.GetInstance().Save();
    }
}

public enum ModeState
{
    Play,
    Pause,

}