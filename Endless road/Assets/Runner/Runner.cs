using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
	public static float distanceTraveled;
	public float acceleration;

	private Vector3 startPosition;
	private bool touchingPlatform;
    
	public  Vector3 jumpVelocity;
	public float gameOverY; //voor het bepalen van de hoogte van de player om game over evnt te triggeren

	void Update () 
	{
        if(touchingPlatform && Input.GetButtonDown("Jump"))
		{
			GetComponent<Rigidbody>().AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingPlatform = false; //zorgt ervoor dat als je de zijkant van platform aanraakt niet meerdere keren kunt springen
		}
		distanceTraveled = transform.localPosition.x;

		if(transform.localPosition.y < gameOverY)//conditie voor triggeren gameover event
		{
			GameEventManager.TriggerGameOver();
		}
	}

	void FixedUpdate () 
	{
		if(touchingPlatform)
		{//als het platform aangeraakt wordt steeds sneller gaan
			GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter () 
	{//detecteren of platform wordt aangeraakt
		touchingPlatform = true;
	}

	void OnCollisionExit () 
	{//detecteren of platform niet meer wordt aangeraakt
		touchingPlatform = false;
	}

	void Start () 
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		GetComponent<Renderer>().enabled = false;//camera wel aan maar runner object zelf niet
		GetComponent<Rigidbody>().isKinematic = true;//niet bewegen voor start van spel
		enabled = false;
	}

	private void GameStart () 
	{
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		GetComponent<Renderer>().enabled = true;//runner object is zichtbaar
		GetComponent<Rigidbody>().isKinematic = false;//beweging zodra het spel start
		enabled = true;
	}
	
	private void GameOver () 
	{
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		enabled = false;
	}

}
