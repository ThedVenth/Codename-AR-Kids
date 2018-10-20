using UnityEngine.UI;
using UnityEngine;

public class ReadGameplayUIManager : MonoBehaviour
{
    public static ReadGameplayUIManager instance;

    public GameObject UIRoot;

    public Slider languageSlider;
    public Button showBigMainContentButton;
    public Text showBigMainContentText;
    public Button showSmallMainContentButton;
    public Text showSmallMainContentText;
    public Button showMainContentButton;
    public Button showSideContentButton;
    public Button nextSideContentButton;
    public Button prevSideContentButton;
    public Button playAudioButton;

    ReadObjectBehaviour currentlyActiveObject;

    private void Awake ()
    {
        if (instance == null)
            Destroy(instance);

        instance = this;

        UIRoot.SetActive(false);
    }

    #region object tracking section

    public void Show (ReadObjectBehaviour _currentlyActive)
    {
        currentlyActiveObject = _currentlyActive;
        UIRoot.SetActive(true);

        OnChangeMinContentKey(_currentlyActive.keyContainer.containedKey);
        ValidateCurrentlyActiveLanguage();
        ValidateMainContentSizeButton(currentlyActiveObject.mainContent.isShowSmallContent);
        ValidateIsShowingSideContent(currentlyActiveObject.isShowingSideContent);
    }

    public void Hide ()
    {
        UIRoot.SetActive(false);
    }
    
    public void SwitchContent ()
    {
        currentlyActiveObject.SwitchContent();
        ValidateIsShowingSideContent(currentlyActiveObject.isShowingSideContent);
    }

    public void SwitchMainContentSize ()
    {
        currentlyActiveObject.SwitchMainContentSize();
        ValidateMainContentSizeButton(currentlyActiveObject.mainContent.isShowSmallContent);
    }

    public void SwitchLanguage ()
    {
        ReadGameplayTrackingManager.instance.ChangeLanguage();
    }

    public void ShowNextSideConent ()
    {
        currentlyActiveObject.ShowNextContent();
    }

    public void ShowPreviousSideContent ()
    {
        currentlyActiveObject.ShowPrevoiusContent();
    }

    public void PlayAudio()
    {
        currentlyActiveObject.PlayAudio();
    }
    #endregion

    #region UI functions
    public void OnLanguageScrollbarChangeValue()
    {
        if (ReadGameplayTrackingManager.instance.isEnglish)
        {
            if (languageSlider.value < 0.5f)
            {
                SwitchLanguage();
                languageSlider.value = 0f;
            }
        }else
        {
            if (languageSlider.value > 0.5f)
            {
                SwitchLanguage();
                languageSlider.value = 1f;
            }
        }
    }

    public void ValidateCurrentlyActiveLanguage()
    {
        languageSlider.value = (ReadGameplayTrackingManager.instance.isEnglish) ? 1 : 0;
    }

    void OnChangeMinContentKey(string _key)
    {
        showBigMainContentText.text = _key.ToUpper();
        showSmallMainContentText.text = _key.ToLower();
    }

    public void ValidateMainContentSizeButton(bool _isShowSmallContent)
    {
        showBigMainContentButton.interactable = _isShowSmallContent;
        showSmallMainContentButton.interactable = !_isShowSmallContent;
    }

    public void ValidateIsShowingSideContent(bool _isShowSideContent)
    {
        showSideContentButton.interactable = !_isShowSideContent;
        showMainContentButton.interactable = _isShowSideContent;
        showBigMainContentButton.gameObject.SetActive(!_isShowSideContent);
        showSmallMainContentButton.gameObject.SetActive(!_isShowSideContent);
        nextSideContentButton.gameObject.SetActive(_isShowSideContent);
        prevSideContentButton.gameObject.SetActive(_isShowSideContent);
    }
    #endregion
}