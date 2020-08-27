using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour, IUnityAdsListener
{
    private const string GameID = "3790119";
    private bool _testMode = false;

    private const string BannerID = "banner";
    private const string VideoRewardID = "rewardedVideo";

    public bool adReady
    {
        get => Advertisement.IsReady();
    }

    public GameController gameController;
    public Player player;

    private void Start()
    {
        Advertisement.Initialize(GameID, _testMode);
        Advertisement.AddListener(this);

        StartCoroutine(ShowBannerWhenReady());
    }

    #region Banner Ad

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(BannerID))
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.Show(BannerID);
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
    }

    #endregion

    #region Video Reward Ad

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            player.Reset();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.

        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == VideoRewardID)
        {

        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
        Debug.LogError(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // Show reward ad
    public void ShowAd()
    {
        if (Advertisement.IsReady() && !gameController.adWatched)
        {
            Advertisement.Show(VideoRewardID);
            gameController.adWatched = true;
        }
    }

    #endregion
}
