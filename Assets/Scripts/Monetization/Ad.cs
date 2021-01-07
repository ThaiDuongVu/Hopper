using UnityEngine;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour
{
    public const string GameID = "3790119";

    private const string BannerID = "banner";
    public const string VideoRewardID = "rewardedVideo";

    private static bool AdReady => Advertisement.IsReady(VideoRewardID);

    private bool bannerShown;

    private void Start()
    {
        Advertisement.Initialize(GameID);
    }

    private void Update()
    {
        if (AdReady && !bannerShown) ShowBanner();
    }

    #region Banner Ad

    // Show banner ad when ready
    private void ShowBanner()
    {
        // Show the banner
        Advertisement.Banner.Show(BannerID);

        // Set banner position to be top center
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

        bannerShown = true;
    }

    #endregion
}
