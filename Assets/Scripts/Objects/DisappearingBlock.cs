using UnityEngine;
using System.Collections;

public class DisappearingBlock : Interactable {

	SpriteRenderer sprite;
	private bool fading;
	private Color og;
	public float recoveryTime;
	public LayerMask mask;
	public override void Start() {
		base.Start ();
		sprite = GetComponent<SpriteRenderer> ();
		fading = false;
		og = sprite == null ? new Color (1, 1, 1, 1) : sprite.color;
	}

	public override void Update () {
		if (sprite == null || !fading)
			return;
		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.025f);
		if (sprite.color.a <= 0) {
			this.gameObject.layer = 0;
			fading = false;
			StartCoroutine (ResetBlock());
		}

	}
	public override void OnObjectEnter(CraycastHit transform) {
		fading = true;
		PlayClip (EnterSound, this.transform.position);
		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.025f);
	}
	public override void OnObjectStay(CraycastHit transform) {
//		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.025f);
	}
	public override void OnObjectExit(CraycastHit transform) {
//		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.025f);

	}

	IEnumerator ResetBlock() {
		yield return new WaitForSeconds (recoveryTime);
		sprite.color = og;
		this.gameObject.layer = 9;

	}


}
