using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameStateDeath : GameState, IUnityAdsListener
{
    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI applesTotal;
    [SerializeField] private TextMeshProUGUI currentApplesTotal;


    [SerializeField] private Image lifeTimer;
    public float timeToDecise = 2.5f;
    private float deathTime;

    private void Start()
    {
        Advertisement.AddListener(this);
    }
    public override void Construct()
    {
        GameManager.Instance.motor.PausePlayer();

        deathTime = Time.time;
        deathUI.SetActive(true);
        

        if (SaveManager.Instance.save.HighScore < (int)GameStat.Instance.score)
        {
            SaveManager.Instance.save.HighScore = (int)GameStat.Instance.score;
            currentScore.color = Color.green;
        }
        else
            currentScore.color = Color.white;
            

        SaveManager.Instance.save.Apples += GameStat.Instance.applesCollectedThisSession;

        SaveManager.Instance.Save();

        highscore.text = "HighScore:" +SaveManager.Instance.save.HighScore;
        currentScore.text = GameStat.Instance.score.ToString("000000");
        applesTotal.text = "Total: " + SaveManager.Instance.save.Apples;
        currentApplesTotal.text = GameStat.Instance.applesCollectedThisSession.ToString();
    }

    public override void Destruct()
    {
        deathUI.SetActive(false);
    }
    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecise;
        lifeTimer.color = Color.Lerp(Color.green, Color.red, ratio);
        lifeTimer.fillAmount = 1 - ratio;

        if (ratio > 1)
            lifeTimer.gameObject.SetActive(false);
    }
    public void TryResumeGame()
    {
        AdManager.Instance.ShowRewardedAd();
    }
    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
       
    }

    public void ToMenu()
    {
        

        brain.ChangeState(GetComponent<GameStateInit>());

        GameManager.Instance.motor.ResetPlayer();
        GameManager.Instance.worldGeneration.ResetWorld();
        GameManager.Instance.SceneChunkGeneration.ResetWorld();

        
    }
    public void EnableRevive()
    {
        lifeTimer.gameObject.SetActive(true);
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        lifeTimer.gameObject.SetActive(false);
        switch(showResult)
        {
            case ShowResult.Failed:
                ToMenu();
                break;
            case ShowResult.Finished:
                ResumeGame();
                break;
            default:
                break;
        }
    }
}
