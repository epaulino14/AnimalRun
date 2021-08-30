using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

   public static AdManager Instance { get { return instance; } }
   private static AdManager instance;
   [SerializeField] private string gameId;
   [SerializeField] private string rewardedVideoPlacementId;
   [SerializeField] private bool testMode; 

    private void Awake()
    {
        instance = this;
        Advertisement.Initialize(gameId, testMode);
    }
    public void ShowRewardedAd()
    {
        ShowOptions so = new ShowOptions();
        Advertisement.Show(rewardedVideoPlacementId, so);
    }

}
