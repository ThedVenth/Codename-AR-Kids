using UnityEngine;

[System.Serializable]
public class ReadObjectMainDataModel
{
    public GameObject bigContentIdObject;
    public GameObject smallContentIdObject;
    public AudioClip idAudioClip;

    public GameObject bigContentEnObject;
    public GameObject smallContentEnObject;
    public AudioClip enAudioClip;

    [HideInInspector] public bool isShowSmallContent;
	
    public void Init()
    {
        ShowSmallContent(false);
    }

    public void ChangeContentSize()
    {
        ShowSmallContent(!isShowSmallContent);
    }

    void ShowSmallContent (bool _isShowSmall)
    {
        isShowSmallContent = _isShowSmall;

        if (ReadGameplayTrackingManager.instance.isEnglish)
        {
            bigContentEnObject.SetActive(!_isShowSmall);
            smallContentEnObject.SetActive(_isShowSmall);
        }
        else
        {
            bigContentIdObject.SetActive(!_isShowSmall);
            smallContentIdObject.SetActive(_isShowSmall);
        }
    }

    public void OnChangeLanguage(bool _isEnglish)
    {
        if(_isEnglish)
        {
            bigContentIdObject.SetActive(false);
            smallContentIdObject.SetActive(false);
        }else
        {
            bigContentEnObject.SetActive(false);
            smallContentEnObject.SetActive(false);
        }

        ShowSmallContent(isShowSmallContent);
    }

    public void PlayAudio ()
    {
        if (ReadGameplayTrackingManager.instance.isEnglish)
            ReadGameplayTrackingManager.instance.PlayAudioOneshot(enAudioClip);
        else
            ReadGameplayTrackingManager.instance.PlayAudioOneshot(idAudioClip);
    }
}