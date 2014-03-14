using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	public GUIStyle centeredGui;
	public GUIStyle heartContainerStyle;
	public Texture2D heartTexture;

	private LevelManager levelManager;
	private bool levelDone = false;
	GUI.WindowFunction windowFunction;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find("Logic Controller").GetComponent<LevelManager>();
		windowFunction = DrawHealth;
	}
	
	void OnGUI () {
		if(levelDone) {
			GUI.Box (new Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "You have escaped the forest!", centeredGui);

			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-25 + 50, 126, 50), "New Adventure +")) {
				// Reset Level
			}
		}

		if(!levelManager.playerAlive()) {
			GUI.Box (new Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "You have died!", centeredGui);

			if(GUI.Button(new Rect (Screen.width/2-63, Screen.height/2-25 + 50, 126, 50), "New Adventure")) {
				levelManager.InitGame();
			}
		}

		GUI.Window (0, new Rect(10, 10, (int)(levelManager.getPlayer().health * 37), 32), windowFunction, "test", heartContainerStyle);
	}

	public void LevelDone() {
		levelDone = true;
	}

	private void DrawHealth(int windowID) {
		for(int i = 0; i < Mathf.CeilToInt(levelManager.getPlayer().health); i += 1) {
			GUI.Box(new Rect(i * 32 + i * 5, 0, 32, 32), heartTexture, heartContainerStyle);
		}
	}
}
