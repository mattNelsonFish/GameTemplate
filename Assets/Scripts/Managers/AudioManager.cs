using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	bool audioManagerReady = false;


	bool pauseAll = false; // if true, every playing sound will be paused // use audiolistener.pause?
	bool muteAll = false; // if true, the volume of all playing clips will be set to 0 // use audiolistener.pause?
	bool clearAll = false; // if true, every playing sound clip will be stopped and cleared from their AudioSource

	AudioSource[] mArrBGM;
	//Dictionary<string, AudioSource> mDictBGM;
	const int MAX_BGM_AUDIO_SOURCES = 3;

	AudioSource[] mArrSFX;
	//Dictionary<string, AudioSource> mDictSFX;
	const int MAX_SFX_AUDIO_SOURCES = 12;

	AudioSource[] mArrVoice;
	//Dictionary<string, AudioSource> mDictVoice;
	const int MAX_VOICE_AUDIO_SOURCES = 5;


	#region initialization
	void Awake() {
		//mDictBGM = new Dictionary<string, AudioSource>();
		mArrBGM = new AudioSource[MAX_BGM_AUDIO_SOURCES];
		for(int i = 0; i < MAX_BGM_AUDIO_SOURCES; i++) {
			mArrBGM[i] = new AudioSource();
		}

		//mDictSFX = new Dictionary<string, AudioSource>();
		mArrSFX = new AudioSource[MAX_SFX_AUDIO_SOURCES];
		for(int i = 0; i < MAX_SFX_AUDIO_SOURCES; i++) {
			mArrSFX[i] = new AudioSource();
		}

		//mDictVoice = new Dictionary<string, AudioSource>();
		mArrVoice = new AudioSource[MAX_VOICE_AUDIO_SOURCES];
		for(int i = 0; i < MAX_VOICE_AUDIO_SOURCES; i++) {
			mArrVoice[i] = new AudioSource();
		}

		audioManagerReady = true;
	}

	// Use this for initialization
	void Start() {
	
	}

	#endregion initialization

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

	/// <summary>
	/// Plays bgm matching the provided string. Will even play if another of the same name is playing.
	/// </summary>
	/// <param name="bgm"></param>
	public void PlayBGM(string bgm) {

	}

	/// <summary>
	/// Pauses all bgm matching the provided string.
	/// </summary>
	public void PauseBGM(string bgm) {

	}

	/// <summary>
	/// Pauses all background music
	/// </summary>
	public void PauseBGM() {

	}

	/// <summary>
	/// Stops all bgm matching the provided string. Stopping frees an AudioSource for use by another bgm.
	/// </summary>
	public void StopBGM(string bgm) {

	}

	/// <summary>
	/// Stops all bgm. Stopping frees AudioSources for use by other bgm.
	/// </summary>
	public void StopBGM() {

	}
}
