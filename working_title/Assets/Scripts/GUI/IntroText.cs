using UnityEngine;
using System.Collections;

public class IntroText : MonoBehaviour {
	private int stage = 0;
	public GUIStyle centeredGui;
	
	void OnGUI () {
		if(stage == 0) {
			GUI.Box (new Rect (Screen.width/2-250, Screen.height/2-250, 500, 100), "What's this? You're looking for a quest to complete? Why, we've got just the adventure for you!", centeredGui);
			
			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-125 + 50, 126, 50), "Continue")) {
				stage ++;
			}
		}

		if (stage == 1) {
			GUI.Box (new Rect (Screen.width/2-250, Screen.height/2-250, 500, 100), "See this thing right here? You are on a quest to find and retrieve it! This... shiny, shiny thing!", centeredGui);
			
			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-125 + 50, 126, 50), "Continue")) {
				stage ++;
			}
		}

		if (stage == 2) {
			GUI.Box (new Rect (Screen.width/2-250, Screen.height/2-250, 500, 100), "This thing is stowed away deep in the thickets of an ancient forest. Countless enemies roam the darkness... You may choose to escape them by compelling the power of WASD, or engage in moderately effective combat using your weapon: Mouse, Destroyer of Worlds!", centeredGui);
			
			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-125 + 50, 126, 50), "Continue")) {
				stage ++;
			}
		}

		if(stage == 3) {
			GUI.Box (new Rect (Screen.width/2-250, Screen.height/2-250, 500, 100), "Additionally, ancient runes may guide you on your quest... but only you can successfully retrieve the shininess and return to the Spawn Point in one piece!", centeredGui);
			
			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-125 + 50, 126, 50), "Begin Adventure")) {
				Application.LoadLevel (1); 
			}
		}
	}
}
