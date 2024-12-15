using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class AdsControl : MonoBehaviour
{


    protected AdsControl()
    {
    }

    private static AdsControl _instance;
  
    public string AdmobID_Android, AdmobID_IOS, BannerID_Android, BannerID_IOS;
    public string UnityID_Android, UnityID_IOS, UnityZoneID;

    public static AdsControl Instance { get { return _instance; } }

    void Awake()
    {
        if (FindObjectsOfType(typeof(AdsControl)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        MakeNewInterstial();
        RequestBanner();
        /*
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
            ShowBanner();
        else
            HideBanner();
         */
       
        DontDestroyOnLoad(gameObject); //Already done by CBManager


    }


    public void HandleInterstialAdClosed(object sender, EventArgs args)
    {

      
        MakeNewInterstial();



    }

    void MakeNewInterstial()
    {




    }


    public void showAds()
    {

    }


    public bool GetRewardAvailable()
    {
        bool avaiable = false;
        return avaiable;
    }

    public void ShowRewardVideo()
    {

      

    }


    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = BannerID_Android;
#elif UNITY_IPHONE
		string adUnitId = BannerID_IOS;
#else
		string adUnitId = "unexpected_platform";
#endif

      

    }

    public void ShowBanner()
    {
       
    }

    public void HideBanner()
    {
      
    }



    public void ShowFB()
    {
       }

    public void RateMyGame()
    {


    }
}

