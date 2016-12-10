using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

//public class NotifyObjectEnter : UnityEvent<RaycastHit2D> { }

[RequireComponent(typeof(ObjectTrigger))]
public class Interactable : MonoBehaviour {


	//this class is just a base class for objects that have a trigger, could have an AudioClip (optional) and Animator(optional). It sets up the trigger system.
	public bool IsInteractable;

	public ObjectTrigger Trigger;
	public AudioClip EnterSound;
	public Animator Animator;

	public virtual void Reset() {
		Trigger = GetComponent<ObjectTrigger> ();
		Animator = GetComponent<Animator> ();
		EnterSound = GetComponent<AudioClip> ();
		IsInteractable = true;
	}

	public virtual void Awake () {
		Trigger = Trigger ? Trigger : GetComponent<ObjectTrigger> ();
		Animator = Animator ? Animator : GetComponent<Animator> ();
		IsInteractable = true;
	}

	public virtual void Start () { 

		Trigger.OnObjectEnter.AddListener (NotifyObjectEnter);
		Trigger.OnObjectStay.AddListener (NotifyObjectStay);
		Trigger.OnObjectExit.AddListener (NotifyObjectExit);

	}

	public virtual void Update() {

	}

	public virtual void FixedUpdate() {

	}

	public void NotifyObjectEnter(CraycastHit collision) {
		OnObjectEnter (collision);
	}
	public void NotifyObjectStay(CraycastHit collision) {
		OnObjectStay (collision);
	}
	public void NotifyObjectExit(CraycastHit collision) {
		OnObjectExit (collision);
	}


	public virtual void OnObjectEnter(CraycastHit collision) {
		this.gameObject.GetComponent<SpriteRenderer> ().color = Color.blue;
	}
	public virtual void OnObjectStay(CraycastHit collision) {
		
	}
	public virtual void OnObjectExit(CraycastHit collision) {
		this.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	public virtual void OnDestroy() {
		Trigger.OnObjectEnter.RemoveListener (NotifyObjectEnter);
		Trigger.OnObjectExit.RemoveListener (NotifyObjectExit);
	}

	public virtual void PlayClip (AudioClip clip, Vector3 source) {
		if(clip != null)
			AudioSource.PlayClipAtPoint (clip, source);

	}
}
