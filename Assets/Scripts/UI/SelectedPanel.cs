using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public struct ViewAlliace
{
    public TeamState state;
    public Sprite img;
}

public class SelectedPanel : Singleton<SelectedPanel>
{
    public Text txtName;
    public TextMeshProUGUI txtPeople;
    public TextMeshProUGUI txtGold;
    public TextMeshProUGUI txtFood;
    public TextMeshProUGUI txtForce;
    public Button potencialItem;
    public Image iconAlliace;
    public CultItem cultItem;
    public RespectItem respectItem;
    public GameUnit origin;
    public List<Button> allPotoncial = new List<Button>();
    public List<CultItem> allCults = new List<CultItem>();
    public List<RespectItem> allRespects = new List<RespectItem>();
    public List<ViewAlliace> viewsAlliace = new List<ViewAlliace>();

    private UnityAction selectedPotencial;

    public void ClearDisplay()
    {
        if (origin != null)
        {
            origin.unitChanged.RemoveListener(UpdateDisplay);
            origin = null;
            txtName.text = "";
            txtPeople.text = "";
            txtGold.text = "";
            txtFood.text = "";
            txtForce.text = "";
            iconAlliace.sprite = null;
            ClearCults();
            ClearPotencial();
            ClearRespects();
        }
    }

    public void ViewUnit(GameUnit unit, Team self)
    {
        ClearDisplay();
        origin = unit;

        origin.unitChanged.AddListener(UpdateDisplay);
        UpdateDisplay();
    }
    protected void UpdateAllinace()
    {
        TeamState state = Unions.instance.GetState(origin.team, GManager.instance.selfTeam);
        for (int i = 0; i < viewsAlliace.Count; i++)
        {
            if (state == viewsAlliace[i].state)
            {
                iconAlliace.sprite = viewsAlliace[i].img;
                break;
            }
        }
    }

    protected void UpdateDisplay()
    {
        txtName.text = origin.SName;
        txtPeople.text = origin.people.humans.Count.ToString();
        txtGold.text = origin.Gold.ToString();
        txtFood.text = origin.food.Quantity.ToString();
        txtForce.text = origin.power.GetPower().ToString();

        UpdateAllinace();
        ClearCults();
        ClearPotencial();
        ClearRespects();
        CreateCults();
        CreateRespects();
        if (Team.Player1 == origin.team)
        {
            CreatePotentials();
        }
    }

    protected void CreatePotentials()
    {
        float distance = 45f;

        for (int i = 0; i < origin.availablePotential.Count; i++)
        {
            Button btnPotencial = Instantiate(potencialItem, potencialItem.transform.parent);
            btnPotencial.GetComponentInChildren<Text>().text = origin.availablePotential[i].name;
            btnPotencial.onClick.AddListener(origin.availablePotential[i].start);
            btnPotencial.transform.position += Vector3.down * i * distance;
            allPotoncial.Add(btnPotencial);
            btnPotencial.gameObject.SetActive(true);
            if (i == 0) selectedPotencial = origin.availablePotential[0].start;
        }
    }

    protected void CreateCults()
    {
        float distance = 45f;

        for (int i = 0; i < origin.people.culture.cults.Count; i++)
        {
            var item = Instantiate(cultItem, cultItem.transform.parent);
            item.sName.text = origin.people.culture.cults[i].name.ToString();
            item.percent.text = ((int)origin.people.culture.cults[i].val).ToString();

            item.transform.position += Vector3.down * i * distance;
            allCults.Add(item);
            item.gameObject.SetActive(true);
        }
    }

    protected void CreateRespects()
    {
        float distance = 45f;
        List<TeamValue> teams = origin.people.GetTeamStatistic();
        for (int i = 0; i < teams.Count; i++)
        {
            var item = Instantiate(respectItem, respectItem.transform.parent);
            item.sName.text = teams[i].team.ToString();
            item.percent.text = (teams[i].count).ToString();

            item.transform.position += Vector3.down * i * distance;
            allRespects.Add(item);
            item.gameObject.SetActive(true);
        }
    }

    protected void ClearPotencial()
    {
        for (int i = 0; i < allPotoncial.Count; i++)
        {
            allPotoncial[i].onClick.RemoveAllListeners();
            Destroy(allPotoncial[i].gameObject);
        }
        allPotoncial.Clear();
    }

    protected void ClearCults()
    {
        for (int i = 0; i < allCults.Count; i++)
        {
            Destroy(allCults[i].gameObject);
        }
        allCults.Clear();
    }

    protected void ClearRespects()
    {
        for (int i = 0; i < allRespects.Count; i++)
        {
            Destroy(allRespects[i].gameObject);
        }
        allRespects.Clear();
    }
}
