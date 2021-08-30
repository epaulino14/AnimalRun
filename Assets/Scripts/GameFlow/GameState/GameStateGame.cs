using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    public GameObject gameUI;
    [SerializeField] private TextMeshProUGUI appleCount;
    [SerializeField] private TextMeshProUGUI ScoreCount;
    public override void Construct()
    {
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        GameStat.Instance.OnCollectedApples += OnCollectApples;
        GameStat.Instance.OnScoreChange += OnScoreChange;

        gameUI.SetActive(true);
    }

    private void OnCollectApples( int amnCollected)
    {
        appleCount.text = amnCollected.ToString("000");
    }

    private void OnScoreChange(float score)
    {
        ScoreCount.text = score.ToString("0000000");
    }
    public override void Destruct()
    {
        gameUI.SetActive(false);

        GameStat.Instance.OnCollectedApples -= OnCollectApples;
        GameStat.Instance.OnScoreChange -= OnScoreChange;
    }
    public override void UpdateState()
    {
        GameManager.Instance.worldGeneration.ScanPosition();
        GameManager.Instance.SceneChunkGeneration.ScanPosition();
    }

   
}
