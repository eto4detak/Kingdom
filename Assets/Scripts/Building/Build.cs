using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : GameUnit, IBuild
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

    public virtual void JoinUnit(GameUnit add)
    {

        People += add.People;
        Gold += add.Gold;
    }

    public T GetPrefab<T>() where T : MonoBehaviour
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


