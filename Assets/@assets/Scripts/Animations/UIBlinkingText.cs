using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UIBlinkingText : MonoBehaviour
{
    public Text toBlink;

    bool isBlinking;
    int direction = 1;
    Color colorContainer;

    void OnEnable()
    {
        Play();
    }

    void OnDisable()
    {
        Stop();
    }

    void Play()
    {
        StartCoroutine(BlinkingCoroutine());
    }

    void Stop()
    {
        isBlinking = false;
    }

    IEnumerator BlinkingCoroutine()
    {
        colorContainer = toBlink.color;
        colorContainer.a = 0;
        toBlink.color = colorContainer;

        isBlinking = true;
        while (isBlinking)
        {
            colorContainer = toBlink.color;
            colorContainer.a += direction * Time.deltaTime;
            toBlink.color = colorContainer;

            if (colorContainer.a >= 1 || colorContainer.a <= 0)
                direction = (direction == 1) ? -1 : 1;

            yield return new WaitForEndOfFrame();
        }
    }
}