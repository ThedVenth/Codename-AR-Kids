using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using System;

public class UITransitionAnimation : MonoBehaviour 
{
	public GameObject content;
	public Vector3 startPos;
	public Vector3 waitPos;
	public Vector3 endPos;

    public float transitionSpeed;

    bool isStratedLoading;

	void Awake()
	{
		content.SetActive(false);
	}
	
    public void StartTransitionInterScene(Action _OnWait = null)
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneCallback;

        StartTransition(() =>
        {
            if (_OnWait != null)
                _OnWait();
        });
    }

    void SceneCallback(Scene _scene, LoadSceneMode _loadmode)
    {
        SceneManager.sceneLoaded -= SceneCallback;
        EndLoading(() => { Destroy(gameObject); });
    }

	public void StartTransition(Action _onWait = null)
	{
		content.transform.position = startPos;
		content.SetActive(true);

        StartCoroutine(MoveCoroutine(waitPos,_onWait));
	}

    IEnumerator MoveCoroutine(Vector3 _targetPos, Action _OnWAIT = null)
    {
        isStratedLoading = true;
        while(Vector2.Distance(content.transform.position,_targetPos) > 0.1f)
        {
           content.transform.position = Vector2.MoveTowards(content.transform.position, _targetPos, transitionSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        if (_OnWAIT != null)
            _OnWAIT();
    }

    public void EndLoading(Action _Callback = null)
    {
        if (!isStratedLoading)
            return;

        StartCoroutine(MoveCoroutine(endPos,_Callback));
    }
}