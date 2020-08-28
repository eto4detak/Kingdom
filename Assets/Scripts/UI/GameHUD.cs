using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;

public class GameHUD : Singleton<GameHUD>
{
    [Header("Images")]
    public GameObject frontImg;
    public GameObject targetImg;
    public GameObject targetList;

    [Header("Commands Buttons")]
    public GameObject btnAttack;
    public GameObject btnMove;
    public GameObject btnStop;

    [Header("Total Statistic")]
    public TextMeshProUGUI totalStar;

    [Header("Team player")]
    public TextMeshProUGUI textPCount;
    public Image playerImage;

    [Header("Team enemy")]
    public TextMeshProUGUI textECount;
    public Image enemyImage;

    [Header("Button")]
    public TextMeshProUGUI txtStartBattle;
    public Button btnStartBattle;
    public TextMeshProUGUI txtRestart;
    public TextMeshProUGUI txtContinue;
    public Button btnContinue;
    public TextMeshProUGUI lvlLabel;

    protected override void Awake()
    {
        base.Awake();
        if(btnStartBattle != null) btnStartBattle.onClick.AddListener(OnStartBattle);
        if(btnContinue != null) btnContinue.onClick.AddListener(OnContinueLevel);
    }

    private void Start()
    {
        SetTotalStar(SaveLoad.GetInstance().pData.star);
        btnContinue.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //textPCount.text = UnitsManager.instance.playerUnits.Count.ToString();
        //textECount.text = UnitsManager.instance.enemyUnits.Count.ToString();
    }


    public void SetTotalStar(int star)
    {
        totalStar.text = star.ToString();
    }

    public void SelectUnit(Unit target) 
    {
      //  frontImg.GetComponent<Button>().image.sprite = target.GetSprite();
      //  speed.text = target.speed.ToString();
        //if (target.command is AttackCommand)
        //{
        //    btnAttack.GetComponent<Button>().image.color = Color.red;
        //    targetImg.GetComponent<Button>().image.sprite = null;
        //    targetList.GetComponentInChildren<Text>().text = "";
        //}
    }


    public void SetTarget<T>(T target) where T : class
    {
        return;
        //if (target is UnitGroup)
        //{
        //    targetImg.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprite/sqareUnit");
        //    targetList.GetComponentInChildren<Text>().text = (target as UnitGroup).name;
        //}
    }


    public void ClearTarget()
    {
        return;
        targetImg.GetComponent<Button>().image.sprite = null;
        targetList.GetComponentInChildren<Text>().text = null;
        
    }
   

    public void ClearPanel()
    {
        frontImg.GetComponent<Button>().image.sprite = null;
        targetImg.GetComponent<Button>().image.sprite = null;
        targetList.GetComponentInChildren<Text>().text = "";
        btnAttack.GetComponent<Button>().image.color = Color.white;
        btnMove.GetComponent<Button>().image.color = Color.white;
        btnStop.GetComponent<Button>().image.color = Color.white;
    }

    public void ViewBtnStart(bool show)
    {
        btnStartBattle.gameObject.SetActive(show);
    }

    public void ViewLvlLabel(bool show)
    {
        lvlLabel.transform.parent.gameObject.SetActive(show);
        if (show)
        {
            lvlLabel.text = "Level " + LevelManager.instance.levelData.levelNumber.ToString();
        }
    }


    private void OnStartBattle()
    {
        LevelManager.instance.ChangeLevelState(LevelManager.LevelState.Play);
        ViewBtnStart(false);
    }

    private void OnContinueLevel()
    {
        //Level.instance.continueGame = true;
        btnContinue.gameObject.SetActive(false);
    }
    

}
