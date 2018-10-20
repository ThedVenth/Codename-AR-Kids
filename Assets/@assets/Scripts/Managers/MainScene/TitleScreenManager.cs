using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public Text versionText;

    public UITransitionAnimation transitionanimation;
    public GameObject mainTitleCanvas;
    public GameObject mainMenuCanvas;

    bool isOnAction;

    void Awake()
    {
        versionText.text = "Ver " + Application.version;
    }

    public void ToMainMenu()
    {
        if (isOnAction)
            return;

        isOnAction = true;
        transitionanimation.StartTransition(() => 
        {
            isOnAction = false;
            mainMenuCanvas.SetActive(true);
            mainTitleCanvas.SetActive(false);

            transitionanimation.EndLoading(() => { });
        });
    }

    public void GoToReadScene()
    {
        if (isOnAction)
            return;

        isOnAction = true;
        transitionanimation.StartTransitionInterScene(() => { SceneManager.LoadSceneAsync("ReadScene"); });
    }
}