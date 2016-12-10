using UnityEngine;
using System.Collections;

public class Spring : Interactable {

	Player player;
	[SerializeField]
	public Side BouncySides;

	public override void Start() {
		base.Start();
		player = player ? player : GameObject.Find ("Player").GetComponent<Player>();
		BouncySides = Side.Top;
	}
	public override void OnObjectEnter(CraycastHit collision) {

		//this is shitty code, it's because I'm not passing an object with more information like TerrainCastHit so I'm just grabbing necessary
		//info to form the functionality I want
		//in this specific case, it's grabbing the players script that controls his velocity, and then I'm doing 
		//a shitty calculation to determine what "side" the controller is on

		if (player != null && !((BouncySides & collision.Side) == 0)) {
			AudioSource.PlayClipAtPoint (EnterSound, collision.Transform.position);
			player.Jump ();
		}
	}
	public override void OnObjectExit(CraycastHit collision) {
		//this is empty on purpose because we don't want the default functionality of the base (Interactable) classes OnObjectExit method.
		//So we override it so nothing happens when onobjectexit triggers!
	}
}
