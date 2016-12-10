using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class ObjectEvent : UnityEvent<CraycastHit> { }

public class ObjectTrigger : MonoBehaviour {

	public ObjectEvent OnObjectEnter;
	public ObjectEvent OnObjectStay;
	public ObjectEvent OnObjectExit;

	public AudioClip Sound;

	public List<CraycastHit> collisions;
	public List<CraycastHit> _notifiedCollisions;


	public void Awake() {

		OnObjectEnter = OnObjectEnter ?? new ObjectEvent ();
		OnObjectStay = OnObjectStay ?? new ObjectEvent ();
		OnObjectExit = OnObjectExit ?? new ObjectEvent ();

		collisions = new List<CraycastHit> ();
		_notifiedCollisions = new List<CraycastHit> ();

	}
		
	public void Start() {

	}

	public virtual void FixedUpdate() {
		//checks if collisions and _notifiedcollisions are empty and ends method so we don't waste cpu on pointless calculations
		if (collisions.Count == 0 && _notifiedCollisions.Count == 0)
			return;

		collisions.RemoveAll (RemoveUnused);

		for (int i = 0; i < collisions.Count; i++) {
			OnObjectStay.Invoke (collisions [i]);
		}

		_notifiedCollisions.Clear ();
	}

	public void NotifyCollision(CraycastHit collision) {
		//checks if the collision is part of last frames collisions
		foreach (CraycastHit hit in collisions) {
			if(hit.Controller == collision.Controller)
				foreach (CraycastHit nhit in _notifiedCollisions) {
					if (nhit.Controller == collision.Controller)
						return;
				}
			_notifiedCollisions.Add (collision);
			return;
		}
		collisions.Add (collision);
		_notifiedCollisions.Add (collision);
		OnObjectEnter.Invoke(collision);
	}

	public bool RemoveUnused(CraycastHit t) {
		foreach (CraycastHit hit in _notifiedCollisions)
			if (hit.Controller == t.Controller)
				return false;
		
		OnObjectExit.Invoke (t);
		return true;
	}


}
