using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI applesText;
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);
        try
        {
            highscoreText.text = "Highscore:  " + SaveManager.Instance.save.HighScore;
            applesText.text = "Apples: " + SaveManager.Instance.save.Apples;
        }
        catch
        {
            Debug.Log("null");
        }
        

        menuUI.SetActive(true);
    }
    public override void Destruct()
    {
        menuUI.SetActive(false);
    }
    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStat.Instance.ResetSession();
        GetComponent<GameStateDeath>().EnableRevive();
    }
    public void OnShopClick()
    {
        brain.ChangeState(GetComponent<GameStateShop>());

    }
}
