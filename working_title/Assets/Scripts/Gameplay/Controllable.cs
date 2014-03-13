using UnityEngine;
using System.Collections;

public class Controllable : MonoBehaviour {
	public float health = 3f;
	public float invulnerabilityTime = 1f;
	public Transform render_model;
	private float invulnCountDown = 0;

	protected virtual void Update () {
		invulnCountDown -= Time.deltaTime;
	}

	public virtual void TakeDamage(float amount) {
		if(invulnCountDown <= 0) {
			health -= amount;
			invulnCountDown = invulnerabilityTime;
			StartCoroutine("FlashRed");
		}

		if(health <= 0f) {
			Destroy(this.gameObject);
		}
	}

	IEnumerator FlashRed() {
		Renderer r;
		if(render_model != null) r = render_model.renderer;
		else r = renderer;

		Color originalColor = r.material.color;
		float rDiff = 1f - r.material.color.r;
		float gDiff = r.material.color.g;
		float bDiff = r.material.color.b;

		for (float i = 0f;  i <= 1f; i += 0.2f) {
			Color newColor = r.material.color;
			newColor.r = originalColor.r + (rDiff * i);
			newColor.g = originalColor.g - (gDiff * i);
			newColor.b = originalColor.b - (bDiff * i);
			r.material.color = newColor;
			yield return new WaitForSeconds(0.02f);
		}

		for (float i = 1f;  i >= 0f; i -= 0.1f) {
			Color newColor = r.material.color;
			newColor.r = originalColor.r + (rDiff * i);
			newColor.g = originalColor.g - (gDiff * i);
			newColor.b = originalColor.b - (bDiff * i);
			r.material.color = newColor;
			yield return new WaitForSeconds(0.02f);
		}

		r.material.color = originalColor;
	}
}
