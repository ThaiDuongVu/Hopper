using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdButton : MonoBehaviour, IUnityAdsListener
{
    private Button button;

    public GameController gameController;
    public Player player;

    private bool playerRewarded;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        // Map the ShowRewardedVideo function to the button’s click listener:
        if (button)
            button.onClick.AddListener(ShowRewardedVideo);

        Advertisement.AddListener(this);
        Advertisement.Initialize(Ad.GameID);
    }

    private void Update()
    {
        // Set interactivity to be dependent on the Placement’s status:
        button.interactable = Advertisement.IsReady(Ad.VideoRewardID) && !gameController.AdWatched;
    }

    // Implement a function for showing a rewarded video ad:
    private static void ShowRewardedVideo()
    {
        Advertisement.Show(Ad.VideoRewardID);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            // Define conditional logic for each ad completion status:
            case ShowResult.Finished:
                // Reward the user for watching the ad to completion.
                if (!playerRewarded)
                    RewardPlayer();
                break;

            case ShowResult.Skipped:
                // Do not reward the user for skipping the ad
                break;

            case ShowResult.Failed:
                // Log the error
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // Reward player for watching the ad
    private void RewardPlayer()
    {
        player.Reset();
        gameController.AdWatched = true;

        playerRewarded = true;
    }
}
