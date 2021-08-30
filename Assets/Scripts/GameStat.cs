using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    public static GameStat Instance { get { return instance; } }
    private static GameStat instance;

    public float score;
    public float highScore;
    public float distanceModifier = 1.5f;
    
    public int totalApples;
    public int applesCollectedThisSession;
    public float pointPerApples = 10;

    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    public Action<int> OnCollectedApples;
    public Action<float> OnScoreChange;

    private void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;
        s += applesCollectedThisSession * pointPerApples;

        if (s > score)
        {
            score = s;
            if(Time.time -lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                OnScoreChange?.Invoke(score);
            }
            
        }
            
    }
    public void CollectApple()
    {
        applesCollectedThisSession++;
        OnCollectedApples?.Invoke(applesCollectedThisSession);
    }

    public void ResetSession()
    {
        score = 0;
        applesCollectedThisSession = 0;

        OnCollectedApples?.Invoke(applesCollectedThisSession);
        OnScoreChange?.Invoke(score);
    }



}
