using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tumbler : Singleton<Tumbler>
{
    public Button tumbler;
    public TublerState state;

    private Text txtTubler;


    protected void Start()
    {
        txtTubler = tumbler.GetComponentInChildren<Text>();
        tumbler.onClick.AddListener(ChangeTumbler);
        ChangeTumbler(TublerState.path);
    }


    public void ChangeTumbler(TublerState data)
    {
        state = data;
        txtTubler.text = state.ToString();
    }

    public void ChangeTumbler()
    {
        if (state == TublerState.bonus)
        {
            state = TublerState.path;
        }
        else
        {
            state = TublerState.bonus;
        }
        txtTubler.text = state.ToString();

    }

}

public enum TublerState
{
    bonus,
    path,
}