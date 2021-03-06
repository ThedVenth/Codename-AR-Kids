﻿using System.Collections.Generic;
using UnityEngine;

public class ReadObjectBehaviour : MonoBehaviour
{
    public TrackableKeyContainer keyContainer;

    public GameObject masterContentRoot;
    public GameObject mainContentRoot;
    public GameObject sideContetnRoot;
    public ReadObjectMainDataModel mainContent;
    public List<ReadObjectDataModel> sideIdContents;
    public List<ReadObjectDataModel> sideEnContents;

    int currentlyActiveIndex;
    [HideInInspector] public bool isShowingSideContent;

    bool isInitted;

    void OnEnable()
    {
        if (!isInitted)
            return;

        ReadGameplayTrackingManager.instance.OnChangeLanguageE += OnChangeLanguage;
    }

    void OnDisable()
    {
        ReadGameplayTrackingManager.instance.OnChangeLanguageE -= OnChangeLanguage;
    }

    void Start()
    {
        isInitted = true;
        OnEnable();
        Stop();
    }

    public void Play ()
    {
        masterContentRoot.SetActive(true);
        HideSideContent();
        mainContent.Init();
        mainContentRoot.SetActive(true);
    }

    public void Stop ()
    {
        HideSideContent();
        mainContentRoot.SetActive(false);
        masterContentRoot.SetActive(false);
    }

    public void SwitchContent ()
    {
        isShowingSideContent = !isShowingSideContent;

        if (isShowingSideContent)
            ShowRandomSideContent();
        else
        {
            HideSideContent();
            mainContentRoot.SetActive(true);
        }
    }

    public void OnChangeLanguage(bool _isEnglish)
    {
        mainContent.OnChangeLanguage(_isEnglish);

        if (isShowingSideContent)
        {
            HideSideContent();
            ShowRandomSideContent();
        }
    }

    public void SwitchMainContentSize ()
    {
        mainContent.ChangeContentSize();
    }

    public void PlayAudio ()
    {
        if (!isShowingSideContent)
            mainContent.PlayAudio();
        else
            PlaySideContentAudio();
    }

    int randomize;
    int tempMaxVal;
    void ShowRandomSideContent()
    {
        sideContetnRoot.SetActive(true);

        tempMaxVal = 
            (ReadGameplayTrackingManager.instance.isEnglish) ?
            sideEnContents.Count : sideIdContents.Count;
        
        randomize = Random.Range(0, tempMaxVal);

        ShowSideContent(randomize);
    }

    public void ShowNextContent()
    {
        HideCurrenlyActiveSideContent();

        currentlyActiveIndex++;
        tempMaxVal = 
            (ReadGameplayTrackingManager.instance.isEnglish) ?
            sideEnContents.Count - 1 : sideIdContents.Count - 1;

        if (currentlyActiveIndex > tempMaxVal)
            currentlyActiveIndex = 0;

        ShowSideContent(currentlyActiveIndex);
    }

    public void ShowPrevoiusContent()
    {
        HideCurrenlyActiveSideContent();

        currentlyActiveIndex--;

        if (currentlyActiveIndex < 0)
            currentlyActiveIndex =
                (ReadGameplayTrackingManager.instance.isEnglish) ?
                sideEnContents.Count - 1 : sideIdContents.Count - 1;

        ShowSideContent(currentlyActiveIndex);
    }

    void ShowSideContent(int _index)
    {
        isShowingSideContent = true;
        currentlyActiveIndex = _index;
        mainContentRoot.SetActive(false);

        if (!ReadGameplayTrackingManager.instance.isEnglish)
            sideIdContents[_index].SetActive(true);
        else
            sideEnContents[_index].SetActive(true);
    }

    void HideCurrenlyActiveSideContent()
    {
        if (!ReadGameplayTrackingManager.instance.isEnglish)
            sideIdContents[currentlyActiveIndex].SetActive(false);
        else
            sideEnContents[currentlyActiveIndex].SetActive(false);
    }

    void HideSideContent()
    {
       foreach (ReadObjectDataModel _obj in sideIdContents)
        {
            _obj.SetActive(false);
        }

       foreach (ReadObjectDataModel _obj in sideEnContents)
        {
           _obj.SetActive(false);
        }

        isShowingSideContent = false;
        sideContetnRoot.SetActive(false);
    }

    void PlaySideContentAudio ()
    {
        if(ReadGameplayTrackingManager.instance.isEnglish)
            ReadGameplayTrackingManager.instance.PlayAudioOneshot(sideEnContents[currentlyActiveIndex].audio);
        else
            ReadGameplayTrackingManager.instance.PlayAudioOneshot(sideIdContents[currentlyActiveIndex].audio);
    }
}