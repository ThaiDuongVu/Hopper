using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour, IUnityAdsListener
{
    public const string GameID = "3790119";

    private const string BannerID = "banner";
    public const string VideoRewardID = "rewardedVideo";

    public static bool adReady => Advertisement.IsReady(VideoRewardID);
    public GameController gameController;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(GameID);

        StartCoroutine(ShowBannerWhenReady());
    }

    #region Banner Ad

    private static IEnumerator ShowBannerWhenReady()
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
        switch (showResult)
        {
            // Define conditional logic for each ad completion status:
            case ShowResult.Finished:
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

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == VideoRewardID)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
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

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    #endregion
}
