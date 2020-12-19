using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    public enum LevelState
    {
        Intro,
        LoadLevel,
        Building,
        Play,
        AllEnemiesSpawned,
        Lose,
        Win
    }

    public float timer;
    public LevelData levelData;
    public UnityEvent playLevel = new UnityEvent();

    public LevelState levelState { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        levelData = new LevelData()
        {
            levelNumber = 1,
            sceneNumber = 1,
        };
    }

    public IEnumerator StartTimer()
    {
        while (true)
        {
            if (levelState == LevelState.Play) timer += Time.deltaTime;
            yield return null;
        }
    }


    public int GetCurrentLevel()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level" + levelNumber);
    }

    public void LoadNextLevel()
    {
        LoadLevel(levelData.levelNumber + 1);
    }

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void ChangeLevelState(LevelState newState)
    {
        if (levelState == newState)
        {
            return;
        }

        LevelState oldState = levelState;
        levelState = newState;
        switch (newState)
        {
            case LevelState.LoadLevel:

                Debug.Log("StartLevel");
                GMode.instance.PauseGame();
                SetFirstSettingsLevel();
                break;
            case LevelState.Play:
                Debug.Log("Play");
                GMode.instance.ContinueGame();
                PlayLevel();
                break;
            case LevelState.AllEnemiesSpawned:
                Debug.Log("AllEnemiesSpawned");

                break;
            case LevelState.Lose:

                Debug.Log("Lose");

                GameOver();
                break;
            case LevelState.Win:

                Debug.Log("win");

                GameComplate();
                break;
        }
    }

    private void SetFirstSettingsLevel()
    {
        ChangeLevelState(LevelState.LoadLevel);
        //GMode.instance.ContinueGame();
        GameHUD.instance.ViewLvlLabel(true);

        MainMenuManager.instance.HideMainMenu();
    }

    private void PlayLevel()
    {
        ChangeLevelState(LevelState.Play);
        playLevel.Invoke();

        MouseManager.instance.enabled = true;

        GameHUD.instance.ViewLvlLabel(false);
        UnitsManager.instance.EventPlayerDead.AddListener(GameOver);
        UnitsManager.instance.EvennEnemyDead.AddListener(GameComplate);

    }

    public void GameComplate()
    {
        Debug.Log("game complete");

        ChangeLevelState(LevelState.Win);
        StopSpawnUnits();
        GameCompleteUI.instance.ShowPanel();

    }
    public void GameOver()
    {
        Debug.Log("game over");

        ChangeLevelState(LevelState.Lose);
        StopSpawnUnits();
        GameOverUI.instance.ShowPanel();
    }

    private void StopSpawnUnits()
    {
        SpawnPoint[] allSpawn = Resources.FindObjectsOfTypeAll<SpawnPoint>();
        for (int i = 0; i < allSpawn.Length; i++)
        {
            allSpawn[i].SetLoop(false);
        }
    }

}

public class LevelData
{
    public int sceneNumber;
    public int levelNumber;
    public int partyCount;
    public int roundCount;
}
