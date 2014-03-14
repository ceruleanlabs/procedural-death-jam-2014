using UnityEngine;
using System.Collections;

public class IntroText : MonoBehaviour {
	private int stage = 0;
	public GUIStyle centeredGui;
	
	void OnGUI () {
		if(stage == 0) {
			GUI.Box (new Rect (Screen.width/2-250, Screen.height/2-250, 500, 100), "You are on a quest to get this thing!", centeredGui);
			
			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-125 + 50, 126, 50), "Continue")) {
				stage ++;
			}
		}
		
		if(stage == 1) {
			GUI.Box (new Rect (Screen.width/2-250, Screen.height/2-250, 500, 100), "MORE TEXT HERE", centeredGui);
			
			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-125 + 50, 126, 50), "Begin Adventure")) {
				Application.LoadLevel (1); 
			}
		}
	}
}
