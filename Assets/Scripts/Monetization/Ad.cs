using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour
{
    public const string GameID = "3790119";

    private const string BannerID = "banner";
    public const string VideoRewardID = "rewardedVideo";

    public static bool adReady => Advertisement.IsReady(VideoRewardID);

    private bool _bannerShown;

    private void Start()
    {
        Advertisement.Initialize(GameID);
    }

    private void Update()
    {
        ShowBanner();
    }

    #region Banner Ad

    // Show banner ad when ready
    private void ShowBanner()
    {
        if (!adReady || _bannerShown) return;

        // Show the banner
        Advertisement.Banner.Show(BannerID);

        // Set banner position to be top center
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

        _bannerShown = true;
    }

    #endregion
}
