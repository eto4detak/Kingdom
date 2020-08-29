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
    public Button btnItem;
    public GameUnit origin;
    public List<Button> allPotoncial = new List<Button>();

    private UnityAction selectedPotencial;


    public void TrySelect(GameUnit obj, Team self)
    {
        ClearDisplay();
        origin = obj;

        if (Unions.instance.CheckAllies(self, origin.team))
        {
            CreateBtnPotentials();
        }
        origin.changed.AddListener(UpdateDisplay);
        UpdateDisplay();
    }

    public bool IsSelected()
    {
        return origin != null;
    }

    public void ClickForOrigin(Vector3 clickPosition)
    {
        selectedPotencial();
    }

    public void CreateBtnPotentials()
    {
        float height = 45f;

        for (int i = 0; i < origin.availablePotential.Count; i++)
        {
            Button btnPotencial = Instantiate(btnItem, transform);
            btnPotencial.GetComponentInChildren<Text>().text = origin.availablePotential[i].name;
            btnPotencial.onClick.AddListener(origin.availablePotential[i].start);
            btnPotencial.transform.position += Vector3.down * i * height;
            allPotoncial.Add(btnPotencial);
            btnPotencial.gameObject.SetActive(true);
            if (i == 0) selectedPotencial = origin.availablePotential[0].start;
        }
    }

    public void ClearDisplay()
    {
        if (origin != null)
        {
            origin.changed.RemoveListener(UpdateDisplay);
            origin = null;
            txtName.text = "";
            txtPeople.text = "";
            txtGold.text = "";

            ClearPotencial();
        }
    }

    protected void UpdateDisplay()
    {
        txtName.text = origin.MyName;
        txtPeople.text = origin.People.ToString();
        txtGold.text = origin.Gold.ToString();
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
}
