using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdInitialize : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private string openUnitId = "ca-app-pub-3940256099942544/3419835294";
    //private string openUnitId = "ca-app-pub-5034189714312969/5980771872";
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
        interstitialAd = new InterstitialAd(openUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
        ShowAd();
    }
    private void OnEnable()
    {
        
    }
    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
}
