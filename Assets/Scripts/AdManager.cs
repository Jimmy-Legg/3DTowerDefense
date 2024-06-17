using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId = "5606616";
    [SerializeField] bool _testMode = false;
    private string _gameId;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    [SerializeField] string _androidBannerAdUnitId = "Banner_Android";
    private string _bannerAdUnitId = null;

    [SerializeField] string _androidInterstitialAdUnitId = "Interstitial_Android";
    private string _interstitialAdUnitId = null;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_ANDROID
        _gameId = _androidGameId;
        _bannerAdUnitId = _androidBannerAdUnitId;
        _interstitialAdUnitId = _androidInterstitialAdUnitId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; // Only for testing the functionality in the Editor
        _bannerAdUnitId = _androidBannerAdUnitId;
        _interstitialAdUnitId = _androidInterstitialAdUnitId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
        else
        {
            OnInitializationComplete();
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAndShowBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadAndShowBannerAd()
    {
        Advertisement.Banner.SetPosition(_bannerPosition);
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(_bannerAdUnitId, options);
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        Advertisement.Banner.Show(_bannerAdUnitId);
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    public void LoadInterstitialAd()
    {
        Advertisement.Load(_interstitialAdUnitId, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(_interstitialAdUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad Loaded: {adUnitId}");
        if (adUnitId == _interstitialAdUnitId)
        {
            ShowInterstitialAd();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"Started showing Ad Unit: {adUnitId}");
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log($"Ad Unit {adUnitId} was clicked.");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad Unit {adUnitId} show completed with state: {showCompletionState}");
    }
}
