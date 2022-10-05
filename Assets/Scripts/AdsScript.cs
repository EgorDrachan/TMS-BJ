using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsScript : MonoBehaviour, IUnityAdsListener
{
   [SerializeField] private bool testMode = true;

    private string gameId = "4957409"; 

    private string video = "Interstitial_Android";
    private string rewardedVideo = "Rewarded_Android";
    private string banner = "Banner_Android";

    void Start() 
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

        #region Banner

        StartCoroutine(ShowBannerWhenInitialized());
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

        #endregion
    }

    public static void ShowAdsVideo(string placementId) 
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementId);
        }
        else
        {
            Debug.Log("Advertisement not ready");
        }
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(banner);
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("ERROR");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ads start");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("Ads finish");
    }
}

