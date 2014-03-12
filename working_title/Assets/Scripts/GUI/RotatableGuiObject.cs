// http://answers.unity3d.com/questions/11022/how-to-rotate-gui-textures.html

using UnityEngine;
public class RotatableGuiObject : MonoBehaviour {
	
	public Texture2D texture = null;
	public float angle = 0;
	public Vector2 size = new Vector2(128, 128);
	Rect rect;
	Vector2 pivot;
	public Transform target;
	
	void Start() {
		rect = new Rect(Screen.width - size.x, Screen.height - size.y, size.x, size.y);
		pivot = new Vector2(rect.xMin + rect.width * 0.5f, rect.yMin + rect.height * 0.5f);
	}

	void OnGUI() {
		if(target != null) {
			angle = target.eulerAngles.y + 90;
			Matrix4x4 matrixBackup = GUI.matrix;
			GUIUtility.RotateAroundPivot(angle, pivot);
			GUI.DrawTexture(rect, texture);
			GUI.matrix = matrixBackup;
		}
	}
}