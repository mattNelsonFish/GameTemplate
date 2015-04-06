using UnityEngine;
using System.Collections;

public abstract class CharacterMover : MonoBehaviour{

	protected Vector3 velocity = Vector3.zero;

	void Awake() {

	}

	abstract protected void Move();
	abstract protected void Follow();
}
