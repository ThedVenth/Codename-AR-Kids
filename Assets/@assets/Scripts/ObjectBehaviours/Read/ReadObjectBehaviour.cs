using System.Collections.Generic;
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
    }

    public void Play ()
    {
        masterContentRoot.SetActive(true);
        HideSideContent();
    }

    public void Stop ()
    {
        masterContentRoot.SetActive(false);
        HideSideContent();
    }

    public void SwitchContent ()
    {
        isShowingSideContent = !isShowingSideContent;

        if (isShowingSideContent)
            ShowRandomSideContent();
        else
            HideSideContent();
    }

    public void OnChangeLanguage(bool _isEnglish)
    {
        mainContent.OnChangeLanguage(_isEnglish);

        if (isShowingSideContent)
            ShowRandomSideContent();
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
        tempMaxVal = 
            (ReadGameplayTrackingManager.instance.isEnglish) ?
            sideEnContents.Count : sideIdContents.Count;
        
        randomize = Random.Range(0, tempMaxVal);

        ShowSideContent(randomize);
    }

    public void ShowNextContent()
    {
        currentlyActiveIndex++;
        tempMaxVal = 
            (ReadGameplayTrackingManager.instance.isEnglish) ?
            sideEnContents.Count : sideIdContents.Count;

        if (currentlyActiveIndex > tempMaxVal)
            currentlyActiveIndex = 0;

        ShowSideContent(currentlyActiveIndex);
    }

    public void ShowPrevoiusContent()
    {
        currentlyActiveIndex--;

        if (currentlyActiveIndex < 0)
            currentlyActiveIndex =
                (ReadGameplayTrackingManager.instance.isEnglish) ?
                sideEnContents.Count : sideIdContents.Count;

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

    void HideSideContent()
    {
        if (!ReadGameplayTrackingManager.instance.isEnglish)
        {
            foreach (ReadObjectDataModel _obj in sideIdContents)
            {
                _obj.SetActive(false);
            }
        }
        else
        {
            foreach (ReadObjectDataModel _obj in sideEnContents)
            {
                _obj.SetActive(false);
            }
        }

        mainContentRoot.SetActive(true);
    }

    void PlaySideContentAudio ()
    {
        if(ReadGameplayTrackingManager.instance.isEnglish)
            ReadGameplayTrackingManager.instance.PlayAudioOneshot(sideEnContents[currentlyActiveIndex].audio);
        else
            ReadGameplayTrackingManager.instance.PlayAudioOneshot(sideIdContents[currentlyActiveIndex].audio);
    }
}