using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTime : Singleton<GameTime>
{
    public TimeDay timeDay;
    public Period seaason;
    public Text txtTime;
    public Text txtDay;
    public Text txtSeasons;
    public UnityEvent timeChanged = new UnityEvent();
    public UnityEvent periodChanged = new UnityEvent();

    private float fullTime;

    public float FullTime
    {
        get => fullTime;
        set {

            fullTime = value;
             }
    }

    protected void Start()
    {
        StartCoroutine(Day());
    }

    protected void FixedUpdate()
    {
        FullTime += Time.fixedDeltaTime;
        ChangeUI();
    }

    public void ChangeUI()
    {
        txtTime.text = ((int)fullTime).ToString();
        txtDay.text = timeDay.ToString();
        txtSeasons.text = seaason.ToString();
    }

    protected IEnumerator Day()
    {
        float duration = 15;
        timeDay = TimeDay.Day;
        timeChanged.Invoke();
        yield return new WaitForSeconds(duration);
        StartCoroutine(Night());
    }

    protected IEnumerator Night()
    {
        float duration = 10;
        timeDay = TimeDay.Night;
        timeChanged.Invoke();
        yield return new WaitForSeconds(duration);
        NextPeriod();
        StartCoroutine(Day());
    }

    protected void NextPeriod()
    {
        switch (seaason)
        {
            case Period.Spring:
                seaason = Period.Summer;
                break;
            case Period.Summer:
                seaason = Period.Autumn;
                break;
            case Period.Autumn:
                seaason = Period.Winter;
                break;
            case Period.Winter:
                seaason = Period.Spring;
                break;
            default:
                break;
        }
    }
    private void Change(int val)
    {
        timeChanged?.Invoke();
    }

}

public enum TimeDay
{
    Day,
    Night,
}

public enum Period
{
    Spring,
    Summer,
    Autumn,
    Winter,
}
