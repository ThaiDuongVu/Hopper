using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdButton : MonoBehaviour, IUnityAdsListener 
{
    private Button _button;

    public GameController gameController;
    public Player player;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        // Set interactivity to be dependent on the Placement’s status:
        _button.interactable = Advertisement.IsReady (Ad.VideoRewardID);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (_button) _button.onClick.AddListener (ShowRewardedVideo);

        Advertisement.AddListener(this);
        Advertisement.Initialize(Ad.GameID);
    }

    // Implement a function for showing a rewarded video ad:
    private static void ShowRewardedVideo () 
    {
        Advertisement.Show (Ad.VideoRewardID);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == Ad.VideoRewardID) {        
            _button.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            // Define conditional logic for each ad completion status:
            case ShowResult.Finished:
                RewardPlayer();
                // Reward the user for watching the ad to completion.
                break;
            case ShowResult.Skipped:
                // Do not reward the user for skipping the ad.
                break;
            case ShowResult.Failed:
                // Log the error
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
        }
    }

    public void OnUnityAdsDidError (string message) 
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    } 

    // Reward player for watching the ad
    private void RewardPlayer()
    {
        player.Reset();
        gameController.adWatched = true;
    }
}
