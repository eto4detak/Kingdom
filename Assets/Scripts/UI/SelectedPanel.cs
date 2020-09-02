using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectedPanel : Singleton<SelectedPanel>
{
    public Text txtName;
    public TextMeshProUGUI txtPeople;
    public TextMeshProUGUI txtGold;
    public Button potencialItem;
    public CultItem cultItem;
    public RespectItem respectItem;
    public GameUnit origin;
    public List<Button> allPotoncial = new List<Button>();
    public List<CultItem> allCults = new List<CultItem>();
    public List<RespectItem> allRespects = new List<RespectItem>();

    private UnityAction selectedPotencial;

    public void ClearDisplay()
    {
        if (origin != null)
        {
            origin.changed.RemoveListener(UpdateDisplay);
            origin = null;
            txtName.text = "";
            txtPeople.text = "";
            txtGold.text = "";
            ClearCults();
            ClearPotencial();
            ClearRespects();
        }
    }

    public void TrySelect(GameUnit obj, Team self)
    {
        ClearDisplay();
        origin = obj;

        origin.changed.AddListener(UpdateDisplay);
        UpdateDisplay();
    }

    protected void CreateCults()
    {
        float distance = 45f;
        
        for (int i = 0; i < origin.culture.cults.Count; i++)
        {
            var item = Instantiate(cultItem, cultItem.transform.parent);
            item.sName.text = origin.culture.cults[i].name.ToString();
            item.percent.text =  ((int)origin.culture.cults[i].val).ToString();
            //cult.img

            item.transform.position += Vector3.down * i * distance;
            allCults.Add(item);
            item.gameObject.SetActive(true);
        }
    }

    protected void CreateRespects()
    {
        float distance = 45f;
        for (int i = 0; i < origin.loyalty.respects.Count; i++)
        {
            var respect = origin.loyalty.respects[i];
            var item = Instantiate(respectItem, respectItem.transform.parent);
            item.sName.text = respect.team.ToString();
            item.percent.text = ((int)respect.val).ToString();

            item.transform.position += Vector3.down * i * distance;
            allRespects.Add(item);
            item.gameObject.SetActive(true);
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

    protected void UpdateDisplay()
    {
        txtName.text = origin.MyName;
        txtPeople.text = origin.People.ToString();
        txtGold.text = origin.Gold.ToString();

        if (Unions.instance.CheckAllies(Team.Player1, origin.team))
        {
            ClearCults();
            CreateCults();
            ClearPotencial();
            CreatePotentials();
            ClearRespects();
            CreateRespects();
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
