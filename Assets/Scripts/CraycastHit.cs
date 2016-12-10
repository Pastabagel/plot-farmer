using UnityEngine;
using System.Collections;

public class CraycastHit {

	public RaycastHit2D Hit;
	//public ControllerSide Side;
	public Controller Controller;
	public Transform Transform;
	public Collider2D Collider;
	public Side Side;
//	public Side side;

	public CraycastHit(RaycastHit2D hit, Controller controller = null, Side side = Side.All) {
		Initialize (hit, controller, side);
	}

	public CraycastHit Initialize(RaycastHit2D hit, Controller controller = null, Side side = Side.All) {

		Hit = hit;
		Controller = controller;
		Side = side;

		if (!hit)
			return this;
		Transform = hit.transform;
		Collider = hit.collider;

		return this;
	}

	public static implicit operator bool(CraycastHit craycastHit) {
		return craycastHit == null ? false : craycastHit.Hit;
	}
}

