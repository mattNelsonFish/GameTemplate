using UnityEngine;
using System.Collections;

public class Player : Character{


	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override protected void notifyManager() {
		CharacterManager.Instance.AddPlayer(this);
	}


}
