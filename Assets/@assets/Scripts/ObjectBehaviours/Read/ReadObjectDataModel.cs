using UnityEngine;

[System.Serializable]
public class ReadObjectDataModel
{
    public string objectName;
    public GameObject gameObject;
    public AudioClip audio;

    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }
}