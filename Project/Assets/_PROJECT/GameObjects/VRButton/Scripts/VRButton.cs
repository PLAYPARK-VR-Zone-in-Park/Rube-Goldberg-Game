﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class VRButton : MonoBehaviour
{
	private Renderer rend;
	public Color normalColor;
	public Color highlightColor;
	public Color clickColor;
	public enum buttonType
	{
		start,
		restartLevel,
		restart,
		exit
	};
	public buttonType type;

	public GameLogic gameLogic;

	// Use this for initialization
	void Start()
	{
		rend = GetComponent<Renderer>();
		rend.material.color = normalColor;
	}

	public void hover()
	{
		Debug.Log("Ray Hit");
		rend.material.color = highlightColor;
	}

	public void click()
	{
		Debug.Log("Button clicked");
		rend.material.color = clickColor;
		if(type == buttonType.start)
		{
			gameLogic.startButton();
		}
	}
}
