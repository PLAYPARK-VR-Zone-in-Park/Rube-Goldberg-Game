﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class OculusControllerInput : MonoBehaviour
{
	//  OCULUS
	public OVRInput.Controller controller;

	//  TELEPORTER   
	private LineRenderer laser;
	public GameObject teleporterAimerObject;
	public GameObject player;
	public LayerMask laserMask;
	public Vector3 teleportLocation;
	public float yNudgeAmount = 1f; /// to teleport a little over the floor/ground
	public float teleportRadius = 15f; /// teleport radius

	//  DASH   
	public float dashSpeed = 0.1f;
	private bool isDashing = false;
	private float lerpTime = 0;
	private Vector3 dashStartPosition;

	//  WALKING   
	public Transform playerCam;
	private Vector3 movementDirection;
	public float moveSpeed = 4f;

	//  GRAB/THROW   
	/*public GameObject blocks;
	public float throwForce = 1.75f;*/

	//   S T A R T                                                                                                      
	void Start ()
	{
		laser = GetComponentInChildren<LineRenderer>();
	}
	
	//   U P D A T E                                                                                                    
	void Update()
	{
		//  TELEPORT   
		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
		{
			Debug.Log("1(A).1: Index finger is being pressed");
			movementDirection = playerCam.transform.forward;
			movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z);
			movementDirection = movementDirection * moveSpeed * Time.deltaTime;
			player.transform.position += movementDirection;
		}

		if(isDashing)
		{
			Debug.Log("2(A).1: Moving");
			lerpTime = Time.deltaTime * dashSpeed;
			player.transform.position = Vector3.Lerp(dashStartPosition, teleportLocation, lerpTime);
			if(lerpTime >= 1)
			{
				isDashing = false;
				lerpTime = 0;
			}
		}
		else
		{
			Debug.Log("2(B).1: Not moving");
			if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
			{
				Debug.Log("2(B).2.1: Index finger is being pressed");
				laser.gameObject.SetActive(true);
				teleporterAimerObject.SetActive(true);

				laser.SetPosition(0, gameObject.transform.position);
				RaycastHit hit;

				if(Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask))
				{
					Debug.Log("2(B).2.2(A): Ray hitting obstacle");
					teleportLocation = hit.point;
					laser.SetPosition(1, teleportLocation);

					// Aimer Position
					teleporterAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);
				}
				else
				{
					Debug.Log("2(B).2.2(B): Ray not hitting anything");
					teleportLocation = (transform.forward * 15) + transform.position;
					RaycastHit groundRay;
					if(Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
					{
						teleportLocation.y = groundRay.point.y;
					}
					laser.SetPosition(1, (transform.forward * 15) + transform.position);

					// Aimer Position
					teleporterAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);
				}
			}
			if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
			{
				Debug.Log("2(B).3.1: Released Index Button");
				laser.gameObject.SetActive(false);
				teleporterAimerObject.SetActive(false);
				///player.transform.position = teleportLocation;
				dashStartPosition = player.transform.position;
				isDashing = true;
			}
		}
	}

	/*void OnTriggerStay(Collider col)
	{
		if(col.gameObject.CompareTag("Throwable"))
		{
			if(device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
			{
				ThrowObject(col);
			}
			else if(device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
			{
				GrabObject(col);
			}
		}
	}

	void GrabObject(Collider coli)
	{
		coli.transform.SetParent(gameObject.transform);
		coli.GetComponent<Rigidbody>().isKinematic = true;
		device.TriggerHapticPulse(2000);
		Debug.Log("Trigger pressed on object");
	}

	void ThrowObject(Collider coli)
	{
		coli.transform.SetParent(blocks.transform);
		Rigidbody rigidBody = coli.GetComponent<Rigidbody>();
		rigidBody.isKinematic     = false;
		rigidBody.velocity        = device.velocity * throwForce;
		rigidBody.angularVelocity = device.angularVelocity;
		Debug.Log("Object thrown");
	}*/
}
