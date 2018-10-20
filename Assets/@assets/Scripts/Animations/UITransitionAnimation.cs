using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransitionAnimation : MonoBehaviour 
{
	public GameObject content;
	public Vector3 startPos;
	public Vector3 waitPos;
	public Vector3 endPos;
	
	void Awake()
	{
		content.SetActive(false);
	}
	
	public void StartTransition()
	{
		content.transform.position = startPos;
		content.SetActive(true);
	}
}