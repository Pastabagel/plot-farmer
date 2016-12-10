using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

public class ColoredBlock : Interactable {

	private SpriteRenderer sprite;
	public enum Colors {
		white,
		red,
		orange,
		yellow,
		green,
		blue,
		violet
	}

	public Colors color;
	public Color c;
	public override void Start() {
		base.Start();
		sprite = GetComponent<SpriteRenderer> ();


		switch (color) {
		case Colors.red:
			c = Color.red;
			break;
		case Colors.orange:
			c = new Color (1, .64f, 0);
			break;
		case Colors.yellow:
			c = new Color(1, 1, 0);
			break;
		case Colors.green:
			c = Color.green;
			break;
		case Colors.blue:
			c = Color.blue;
			break;
		case Colors.violet:
			c = Color.magenta;
			break;
		default:
			c = Color.white;
			break;
		}

	}
	public override void OnObjectEnter(CraycastHit collision) {
		//this method overrides the InteractableTerrain method "OnObjectEnter" which the trigger system uses by default upon collision.
		//this means that any class that inherits InteractableTerrain such as this one, can override that method like I am here and do its own interaction
		//in this case, it's playing this objects sound clip which was manually attached to the EnterSound variable in the inspector which was inherited from InteractableTerrain.cs
		//then it sets the sprite color of this objects sprite renderer to the color manually set in the inspector, limited to the colors I set in the enum.
		if (EnterSound != null) {
			AudioSource.PlayClipAtPoint (EnterSound, collision.Transform.position);
		}
		sprite.color = c;

	}

	public override void OnObjectExit(CraycastHit collision) {
		//same as onobjectenter comment except for onobjectexit event. This overrides the default one in the inherited class InteractableTerrain.cs. You can look at that ones functionality, and realize that
		//since this method overrides it, that functionality is thrown out and replaced with whatever is in this method which overrides it.
		sprite.color = Color.white;
	}
}
