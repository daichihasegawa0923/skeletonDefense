using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobDisplayer : MonoBehaviour
{

    // ca-app-pub-5412357127817198~3547928777

    // ca-app-pub-5412357127817198/9123689087

    // sample
    // ca-app-pub-3940256099942544/1033173712

    [SerializeField]
    private bool _isAndroid = true;

    [SerializeField]
    private string _unitIdAndroid = "";

    [SerializeField]
    private string _unitIdiOS = "";

    private InterstitialAd _interstitial;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(i => { });
        RequestInterstitialAds();

    }

    private void RequestInterstitialAds()
    {
        this._interstitial = new InterstitialAd(_isAndroid ? _unitIdAndroid : _unitIdiOS);
        
        this._interstitial.OnAdLoaded += (sender, args) =>
        {
            Debug.Log("Ad is loaded");
            var isShow = Random.Range(0, 10) > 5;
            if (isShow)
                this._interstitial.Show();
        };

        this._interstitial.OnAdFailedToLoad += (sender, args) =>
        {
            Debug.Log("Ad load failed.");
        };

        var request = new AdRequest.Builder().Build();
        this._interstitial.LoadAd(request);
    }
}
