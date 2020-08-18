using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitColor : MonoBehaviour
{
    public List<Renderer> forColor;
    public Color unitColor;

    private float startOpacity = 0.4f;
    private float currentOpacity;
    private float deltaOpacity = 0.01f;
    private Material unitMaterial;
    private Unit owner;
    private bool flicker;

    private void Start()
    {
        owner = GetComponent<Unit>();
        Material tempM = UnitsManager.instance.GetTeamColor(owner.GetTeam());
        ChangeMaterial(tempM);
        currentOpacity = startOpacity;
        unitMaterial = GetComponent<Renderer>().material;
        unitColor = unitMaterial.color;
    }

    public void UpdateColor()
    {
        if (flicker) return;
        Color tempColor = unitColor;
        if (currentOpacity > 1) currentOpacity = 1;
        if (currentOpacity < startOpacity) currentOpacity = startOpacity;
        tempColor.a = currentOpacity;
        //TO DO
        ChangeColor(tempColor);
    }

    public void SetColor(float percent)
    {
        currentOpacity = startOpacity + (1-startOpacity) * percent;
    }

    public void UpColor()
    {
        currentOpacity += deltaOpacity;
    }

    public void ResetColor()
    {
        currentOpacity = startOpacity;
    }

    public void DownColor()
    {
        currentOpacity -= deltaOpacity;
    }

    public void StartFlicker()
    {
        StartCoroutine(Flicker());
    }

    public void ChangeColor(Color color)
    {
        unitMaterial.SetColor("_Color", color);
    }

    public void NoFlicker()
    {
        flicker = false;
    }

    private IEnumerator Flicker()
    {
        flicker = true;
        ChangeColor(UnitsManager.instance.flickerMaterial.color);
        yield return new WaitForSeconds(1.5f);
    }

    private void ChangeMaterial(Material newColor)
    {
        if (forColor != null)
        {
            for (int i = 0; i < forColor.Count; i++)
            {
                forColor[i].material = newColor;
            }
        }
    }

}
