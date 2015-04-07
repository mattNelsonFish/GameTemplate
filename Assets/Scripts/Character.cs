using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	protected CharacterMover m_charMover;
	void Awake() {
		notifyManger();
	}

	protected void notifyManger() {
		CharacterManager.Instance.AddCharacter(this);
	}
}
