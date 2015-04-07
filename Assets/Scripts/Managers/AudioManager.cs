using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	#region BGM control
	/// <summary>
	/// returns -1 if no slot found. Otherwise the int index of the the bgm open slot
	/// </summary>
	private int locateOpenBGMsource() {
		int openBGMSlot = -1;
		bool found = false;
		for(int i = 0; i < MAX_BGM_AUDIO_SOURCES && !found; i++ ) {
			if(mArrBGM[i].clip == null) {
				openBGMSlot = i;
				found = true;
			}
		}

		return openBGMSlot;
	}

	#region locateActiveBGM
	/// <summary>
	/// returns -1 if no active BGM matches. Otherwise the int will show the first
	/// </summary>
	private int locateActiveBGM(string clipName) {
		return locateActiveBGM(0, clipName);
	}

	private int[] locateAllActiveBGM(string clipName) {
		List<int> allWithName = new List<int>();
		int result = -2;
		int startIndex = 0; ;
		while(result != -1) {
			result = locateActiveBGM(startIndex, clipName);
			if(result != -1){
				allWithName.Add(result);
				startIndex = result + 1;
			}
		}

		return allWithName.ToArray();
	}

	/// <summary>
	/// returns -1 if no active BGM matches. Otherwise the int will show the first clip matching the name that
	/// is located after the provided startIndex
	/// </summary>
	private int locateActiveBGM(int startIndex, string clipName) {
		int activeBGMSlot = -1;
		bool found = false;
		for(int i = startIndex; i >= 0 && i < MAX_BGM_AUDIO_SOURCES && !found; i++) {
			if(mArrBGM[i].clip == null) {
				if(mArrBGM[i].clip != null && mArrBGM[i].clip.name == clipName) {
					activeBGMSlot = i;
					found = true;
				}
			}
		}

		return activeBGMSlot;
	}

	#endregion locateActiveBGM

	/// <summary>
	/// Plays bgm matching the provided string. Will even play if another of the same name is playing. Will not play if name is unknown.
	/// </summary>
	/// <param name="bgm"></param>
	/// TODO ask if it should be unique
	public void PlayBGM(string bgm) {
		int bgmSourceSlot = locateOpenBGMsource();
		if(bgmSourceSlot != -1) {
			AudioClip clip = AssetManager.Instance.GetClip(bgm);
			if(clip != null) {
				mArrBGM[bgmSourceSlot].clip = clip;
				mArrBGM[bgmSourceSlot].Play();
			}
		}
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
	#endregion BGM control
}
