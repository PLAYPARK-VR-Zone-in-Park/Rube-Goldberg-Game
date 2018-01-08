﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
	public GameObject player;
	private ControllerInput controllerInput;
	///public GameObject l_controller_GO;
	public GameObject r_controller_GO;
	public int currentLevel;
	public int startLevel;

	//  Scene Changing
	private enum SceneTransition
	{
		starting,
		complete,
		ending
	};
	private SceneTransition sceneTransition;
	public GameObject l_mask;
	public GameObject r_mask;
	private Renderer l_maskRend;
	private Renderer r_maskRend;
	public float sceneChangingTime;
	private float endTime;
	private float startTime;

	//   S T A R T                                                                                                      
	void Start()
	{
		Debug.Log("GameLogic started");
		controllerInput = player.GetComponent<ControllerInput>();
		l_maskRend = l_mask.GetComponent<Renderer>();
		r_maskRend = r_mask.GetComponent<Renderer>();
		sceneTransition = SceneTransition.starting;
		if(currentLevel == 0)
		{
			Debug.Log("Game started!");
			initGame();
		}
		else
		{
			Debug.Log("Level " + currentLevel + " started!");
		}
		initSceneChange();
	}

	private void initGame()
	{
		Debug.Log("Game initiated!");
	}

	public void startButton()
	{
		Debug.Log("Start Button pressed!");
		currentLevel = startLevel;
		sceneTransition = SceneTransition.ending;
		initSceneChange();
		///SceneManager.LoadScene("level" + currentLevel);
	}

	private void initSceneChange()
	{
		Debug.Log("Changing Scene Anim Started");
		l_mask.SetActive(true);
		r_mask.SetActive(true);
		///l_controller_GO.SetActive(false);
		r_controller_GO.SetActive(false);
		startTime = Time.time;
		endTime = startTime + sceneChangingTime;
		Debug.Log("1. Current Time is " + startTime);
		Debug.Log("1. End Time is " + endTime);
	}
	
	//   U P D A T E                                                                                                    
	void Update()
	{
		if(sceneTransition == SceneTransition.starting)
		{
			float fraction = 1f - ((Time.time - startTime) / sceneChangingTime);
			Debug.Log("2. Now the time is " + Time.time);
			Debug.Log("2. Elapsed time: " + (Time.time - startTime));
			Debug.Log("2. fraction: " + fraction);
			Color color = l_maskRend.material.color;
			color = new Color(color.r, color.g, color.b, fraction);
			l_maskRend.material.color = color;
			r_maskRend.material.color = color;
			if(fraction <= 0f)
			{
				Debug.Log("3. Scene Anim Complete");
				sceneTransition = SceneTransition.complete;
				l_mask.SetActive(false);
				r_mask.SetActive(false);
				///l_controller_GO.SetActive(true);
				r_controller_GO.SetActive(true);
			}
		}
		else if(sceneTransition == SceneTransition.ending)
		{
			float fraction = (Time.time - startTime) / sceneChangingTime;
			Color color = l_maskRend.material.color;
			color = new Color(color.r, color.g, color.b, fraction);
			l_maskRend.material.color = color;
			r_maskRend.material.color = color;
			if(fraction >= 1f)
			{
				SceneManager.LoadScene("level" + currentLevel);
			}
		}
		else
		{
			controllerInput.checkInput();
		}
	}

	public void teleport(Vector3 teleportLocation)
	{
		if(currentLevel != 0)
		{
			Debug.Log("Teleporting to: " + teleportLocation);

		}
	}
}
