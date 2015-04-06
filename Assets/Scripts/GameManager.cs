using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	private bool m_bIsPaused = false;
	public bool IsPaused {
		get {
			return m_bIsPaused;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		//Logger.Instance.LogComment("I'm a comment.");
		//Logger.Instance.LogWarning("I'm warning against that.");
		//Logger.Instance.LogError("Realy bad things just happened. Thought you ought to know.");
	}

	// pauses game.
	public void Pause() {
		m_bIsPaused = true;
	}

	// unpauses game.
	public void Unpause() {
		m_bIsPaused = false;
	}
}
