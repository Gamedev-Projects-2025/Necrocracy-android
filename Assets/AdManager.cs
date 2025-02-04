using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Unity Ads Settings")]
    public string gameId = "5784229"; // Replace with your Unity Ads game ID
    public string bannerPlacement = "Banner_Android"; // Banner placement
    public string interstitialPlacement = "Interstitial_Android"; // Interstitial placement
    public string rewardPlacement = "Rewarded_Android"; // Rewarded video placement
    public bool testMode = false; // Set to false for production

    private void Start()
    {
        // Initialize Unity Ads
        InitializeAds();
    }

    private void InitializeAds()
    {
        // Initialize Unity Ads and set this class as the listener for initialization.
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(interstitialPlacement);
        }
        else
        {
            Debug.Log("Interstitial ad not ready.");
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(rewardPlacement);
        }
        else
        {
            Debug.Log("Rewarded ad not ready.");
        }
    }

    public void ShowBannerAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(bannerPlacement);
        }
        else
        {
            Debug.Log("Banner ad not ready.");
        }
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    #region Unity Ads Initialization Listener

    // Called when Unity Ads is successfully initialized
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    // Called when Unity Ads initialization fails
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError("Unity Ads initialization failed: " + message);
    }

    #endregion

    #region Unity Ads Show Listener

    // Called when an ad starts showing
    public void OnShowStart(string placementId)
    {
        Debug.Log("Ad started: " + placementId);
    }

    // Called when an ad finishes showing
    public void OnShowFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardPlacement)
        {
            if (showResult == ShowResult.Finished)
            {
                // Reward the user
                Debug.Log("User watched the full ad, rewarding...");
                RewardUser();
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Handle ad skip
                Debug.Log("User skipped the ad.");
            }
            else if (showResult == ShowResult.Failed)
            {
                // Handle ad failure
                Debug.LogError("Ad failed to show.");
            }
        }
    }

    // Called when an ad fails to show
    public void OnShowFailed(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError("Ad failed to show: " + message);
    }

    #endregion

    // Reward the user for watching the ad
    private void RewardUser()
    {
        // Implement your reward logic here (e.g., giving coins or unlocking features)
        Debug.Log("User rewarded with coins!");
    }


}
