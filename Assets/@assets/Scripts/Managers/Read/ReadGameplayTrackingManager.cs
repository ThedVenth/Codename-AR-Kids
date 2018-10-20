using System.Collections.Generic;
using UnityEngine;
using System;

public class ReadGameplayTrackingManager : MonoBehaviour 
{
	public static ReadGameplayTrackingManager instance;

    public AudioSource audioSource;

	[HideInInspector] public List<ReadObjectBehaviour> trackedObjectList = new List<ReadObjectBehaviour>();
    [HideInInspector] public bool isEnglish;

    public event Action<bool> OnChangeLanguageE;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else
			Destroy(gameObject);
	}
	
	public void AddTrackedObject(ReadObjectBehaviour _obj)
	{
		if(!trackedObjectList.Contains(_obj))
			trackedObjectList.Add(_obj);

        ValidateWhichShouldPlay();
	}
	
	public void RemoveTrackedObject(ReadObjectBehaviour _obj)
	{
		if(trackedObjectList.Contains(_obj))
			trackedObjectList.Remove(_obj);

        _obj.Stop();
        ValidateWhichShouldPlay();
    }

    public void ChangeLanguage()
    {
        isEnglish = !isEnglish;

        if (OnChangeLanguageE != null)
            OnChangeLanguageE(isEnglish);
    }

    public void PlayAudioOneshot(AudioClip _audioClip)
    {
        audioSource.PlayOneShot(_audioClip);
    }

    void ValidateWhichShouldPlay()
    {
        if (trackedObjectList.Count > 0)
        {
            trackedObjectList[0].Play();
            ReadGameplayUIManager.instance.Show(trackedObjectList[0]);
        }else
        {
            ReadGameplayUIManager.instance.Hide();
        }
    }
}