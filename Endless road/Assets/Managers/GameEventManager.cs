using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager
{
    public delegate void GameEvent();
    public static event GameEvent GameStart, GameOver; //event bij wanneer de game start en eindigt

    	public static void TriggerGameStart(){ //methode voor het triggeren van GameStart event
		if(GameStart != null){
			GameStart();
		}
	}

	public static void TriggerGameOver(){//methode voor het triggeren van GameOver event
		if(GameOver != null){
			GameOver();
		}
	}

}
