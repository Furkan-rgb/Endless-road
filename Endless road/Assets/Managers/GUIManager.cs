using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	public Text gameOverText, instructionsText, runnerText;


    	void Start () //Game over text uitschakelen op het begin van het spel
        {
            GameEventManager.GameStart += GameStart;
            GameEventManager.GameOver += GameOver;
		    gameOverText.enabled = false;
	    }

	void Update () 
    {
		if(Input.GetButtonDown("Jump"))//game start event triggeren zodra er gesprongen wordt dus spatie of x gedrukt wordt
        {
			GameEventManager.TriggerGameStart();
		}
	}

    	private void GameStart () //game start event handler
        {
		    gameOverText.enabled = false;
		    instructionsText.enabled = false;
		    runnerText.enabled = false;
		    enabled = false; //de handler zelf ook uitzetten anders bij elke jump zou er een game start event plaatsvinden
	    }

        private void GameOver () 
        {
		    gameOverText.enabled = true;
		    instructionsText.enabled = true;
		    enabled = true;
	    }
}
