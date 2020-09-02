using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locality : GameUnit
{
    private bool isStart;

    protected void Start()
    {
        LevelManager.instance.playLevel.AddListener(StartLife);
    }

    protected void OnDisable()
    {
        if (isStart)
        {
            StopLife();
        }
    }

    public void StartLife()
    {
        if (!isStart)
        {
            StartCoroutine(Life());
        }
    }

    public virtual void JoinUnit(GameUnit traveler)
    {
        if (Unions.instance.CheckEnemies(traveler.team, team)) return;

        People += traveler.People;
        Gold += traveler.Gold;
        bool changeCult = culture.Merger(traveler.culture);
        team = loyalty.Merger(traveler.loyalty);

        Destroy(traveler.gameObject);
    }

    protected T CreateFormation<T>(int countPeople) where T : GameUnit
    {
        int minPeople = 20;
        if (People < minPeople + countPeople) return null;

        People -= countPeople;
        culture.Development(-countPeople);
        var prefab = GetPrefab<T>();
        T formation = Instantiate(prefab, transform.position, Quaternion.identity);
        formation.People = countPeople;
        formation.loyalty.ChangeLoyalty(new Respect() {team = team, val = countPeople });
        return formation;
    }

    protected T GetPrefab<T>() where T : MonoBehaviour
    {
        return Resources.Load<T>("Units/" + typeof(T));
    }

    protected virtual void Development()
    {
    }

    protected IEnumerator Life()
    {
        isStart = true;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Development();
        }
    }

    protected void StopLife()
    {
        StopCoroutine(Life());
        isStart = false;
    }
}


