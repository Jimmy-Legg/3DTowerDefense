using UnityEngine;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine.UI;
using System;

public class AdmobAdsScript : MonoBehaviour
{
    public string appID = "ca-app-pub-1459246688566339~8502687180";


#if UNITY_ANDROID 
    string interstitialID = "ca-app-pub-1459246688566339/9823840853";
    string bannerID = "ca-app-pub-1459246688566339/7395922409";
#endif

    BannerView bannerView;
    InterstitialAd interstitial;

    private void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        MobileAds.Initialize(initStatus =>
        {
            print("Admob initialized");

            LoadBanner();
            LoadInterstitial();
        });
    }

    #region Banner
    public void LoadBanner()
    {
        CreateBannerView();
        ListenToBannerEvents();

        if (bannerView != null)
        {
            CreateBannerView();
        }

        var request = new AdRequest();
        request.Keywords.Add("unity-admod-sample");

        print("Loading banner ad");
        bannerView.LoadAd(request);
    }

    private void ListenToBannerEvents()
    {
        // Raised when an ad is loaded into the banner view.
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Banner view paid {0} {1}."+
                adValue.Value+
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    private void CreateBannerView()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            print("Destroying banner");
            bannerView.Destroy();
            bannerView = null;
        }
    }  
    #endregion

    #region Interstitialad

    public void LoadInterstitial()
    {
        if (interstitial != null)
        {
            interstitial = null;
        }
        var request = new AdRequest();
        request.Keywords.Add("unity-admod-sample");

        InterstitialAd.Load(interstitialID, request, (ad, error) =>
        {
            if (error != null)
            {
                Debug.LogError("Failed to load interstitial ad with error: " + error);
                return;
            }

            interstitial = ad;
            Debug.Log("Interstitial ad loaded");
            InterstitialEvents(interstitial);
        });

    }

    public void ShowInterstitial()
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready yet");
        }
    }

    public void InterstitialEvents(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Interstitial ad paid {0} {1}."+
                adValue.Value+
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };

    }
        
        
        
        #endregion
}
