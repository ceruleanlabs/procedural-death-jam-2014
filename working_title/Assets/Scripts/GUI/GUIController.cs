using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	public GUIStyle centeredGui;

	private LevelManager levelManager;
	private bool levelDone = false;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find("Logic Controller").GetComponent<LevelManager>();
	}
	
	void OnGUI () {
		if(levelDone) {
			GUI.Box (new Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "You have escaped the forest!", centeredGui);

			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-25 + 50, 126, 50), "New Adventure +")) {
				// Reset Level
			}
		}

	}

	public void LevelDone() {
		levelDone = true;
	}
}
