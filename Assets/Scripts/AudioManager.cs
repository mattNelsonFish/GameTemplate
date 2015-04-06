using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	bool audioManagerReady = false;


	bool pauseAll = false; // if true, every playing sound will be paused
	bool muteAll = false; // if true, the volume of all playing clips will be set to 0
	bool clearAll = false; // if true, every playing sound clip will be stopped and cleared from their AudioSource

	const int MAX_NUM_AUDIO_SOURCES = 20;
	AudioSource[] audioSources;



	void Awake() {
		audioSources = new AudioSource[MAX_NUM_AUDIO_SOURCES];
		for(int i = 0; i < MAX_NUM_AUDIO_SOURCES; i++) {
			audioSources[i] = new AudioSource();
		}

		audioManagerReady = true;
	}

	// Use this for initialization
	void Start() {
	
	}

	public IEnumerator PlayClip(string clipName) {
		// check for open audiosource

		// if there is, find clip

		// if found, play till it's completed
		//while(false){
			yield return null;
	//	}

	}

	private AudioClip getClip(string name) {
		AudioClip ac = null;
		switch(name){
			case "boom":
				// make ac the right clip
				break;
			default:
				break;
		}

		return ac;
	}
}
