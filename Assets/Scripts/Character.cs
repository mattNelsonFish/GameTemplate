using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	protected CharacterMover m_charMover;
	void Awake() {
		notifyManger();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	abstract protected void notifyManger() {
		CharacterManager.Instance.AddCharacter(this);
	}
}
