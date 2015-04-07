using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

public class AssetBundleManager : MonoBehaviour {

	#region variables
	private Dictionary<string, WWW> loadingDict;
	private Dictionary<string, AssetBundle> bundleDict;
	private const int version = 14;

	private GUIStyle loadBarFill = null;
	private GUIStyle loadBarBorder_Middle;
	private GUIStyle loadBarBorder_RightCap;
	private GUIStyle loadBarBorder_LeftCap;
	private GUIStyle loadBarFill_RightCap;
	private GUIStyle loadBarFill_LeftCap;
	private GUIStyle loadBarBack;
	private GUIStyle loadBarBack_LeftCap;
	private GUIStyle loadBarBack_RightCap;
	private GUIStyle loadBarBack_Middle;
	private GUIStyle loadBarTravellingIcon; // the blimp on the ProgressBar

	//GameObject genericLoadingScreen;
	#endregion variables

	#region bundleProgressEnum
	public enum BundleLoadStatus {
		LOADED, LOADING, NOTLOADED, NONEXISTANT,  UNKNOWN
	}
	#endregion

	#region Singleton Implemenation
	private static AssetBundleManager instance = null;
	public static AssetBundleManager Instance {
		get {
			if(instance == null) {
				instance = (new GameObject("AssetBundleManager")).AddComponent<AssetBundleManager>();
			}
			return instance;
		}
	}
	#endregion Singleton Implemenation

	void Awake() {
		if(instance != null) {
			Destroy(gameObject);
		}
		/*
		GUISkin ookaSkin = Resources.Load("GUI/ookaSkin") as GUISkin;
		
		loadBarFill = ookaSkin.GetStyle("LoadBarFill");
		loadBarBorder_Middle = ookaSkin.GetStyle("LoadBarBorder_Middle");
		loadBarBorder_RightCap = ookaSkin.GetStyle("LoadBarBorder_RightCap");
		loadBarBorder_LeftCap = ookaSkin.GetStyle("LoadBarBorder_LeftCap");
		loadBarFill_RightCap = ookaSkin.GetStyle("LoadBarFill_RightCap");
		loadBarFill_LeftCap = ookaSkin.GetStyle("LoadBarFill_LeftCap");
		loadBarBack = ookaSkin.GetStyle("LoadBarBack");
		loadBarTravellingIcon = ookaSkin.GetStyle("LoadBarBlimp");
		loadBarBack_LeftCap = ookaSkin.GetStyle("LoadBarBack_leftCap");
		loadBarBack_RightCap = ookaSkin.GetStyle("LoadBarBack_RightCap");
		loadBarBack_Middle = ookaSkin.GetStyle("LoadBarBack_Middle");
		*/
		DontDestroyOnLoad(this.gameObject);
		bundleDict = new Dictionary<string, AssetBundle>();
		loadingDict = new Dictionary<string, WWW>();
		loadingThisRun = new List<string>();

	}

	int mNumLoading;
	string mLoadVal;
	bool showGUI = false;
	float avgProg = 0;
	List<string> loadingThisRun;
	void Update() {
		if(outputBack == null) {
			outputBack = new Texture2D(4, 4);
			// this color change doesn't work for some reason.
			for(int i = 0; i < outputBack.width; i++) {
				for(int j = 0; j < outputBack.height; j++) {
					outputBack.SetPixel(i, j, Color.black);
				}
			}
		}
		float totalProg = 0f;
		// hopping load bar. shows progress of remaining.
		// shows lots is happening, but harder to grasp when it'll actually be done
		//hopperLoadingBar(ref mNumLoading, ref totalProg);

		// slow climb load bar. Shows progress since loading began this run.
		// shows overall progress since last reset (occurs at 100%).
		// may hop back a little as new bundles are added as others are loading
		gradualLoadingBar(ref mNumLoading, ref totalProg);

		
		if(loadingDict.Count > 0) {

			avgProg = (totalProg / (float)mNumLoading);

			mLoadVal = (avgProg * 100f).ToString("F2");
			showGUI = true;
			delayOnHide = DELAY_ON_HIDE;
		}
		else {
			mLoadVal = "100.00";
		}
		if(delayOnHide < 0) {
			showGUI = false;
			loadingThisRun.Clear();
		}
		else {
			delayOnHide -= Time.deltaTime;
		}
	}

	private void hopperLoadingBar(ref int numLoading, ref float totalProg){
		mNumLoading = loadingDict.Count;
		foreach(WWW www in loadingDict.Values) {
			totalProg += www.progress;
		}
	}

	private void gradualLoadingBar(ref int numLoading, ref float totalProg){
		// compares known loads with currently loading. If loading something that isn't known, add it to the list
		string[] knownKeys = new List<string>(loadingDict.Keys).ToArray();
		for(int i = 0; i < knownKeys.Length; i++) {
			if(!loadingThisRun.Contains(knownKeys[i])){
				loadingThisRun.Add(knownKeys[i]);
			}
		}

		mNumLoading = loadingThisRun.Count;
		totalProg = 0f;
		float completedCount = loadingThisRun.Count - loadingDict.Count; // the ones that are at 100%, so they're not in the loadingDict
		foreach(WWW www in loadingDict.Values) {
			totalProg += www.progress;
		}
		totalProg += completedCount;
	}

	float delayOnHide = 0;
	const float DELAY_ON_HIDE = 2f;
	GUIStyle outputStyle;
	Texture2D outputBack;
	void OnGUI() {
		GUI.depth = -110;
		if(showGUI) {
			/*
			float barWidth = AspectRatioFixer.SimulatedWidth * 0.25f;
			float barHeight = 19;//38;
			float endCapWidth = 9;//19;
			float barBorderHeight = 21; // 41
			float barBorderWidth = AspectRatioFixer.SimulatedWidth * 0.25f; //368;
			float barBorderEndCapWidth = 11;//20;

			float blimpWidth = 101;
			float blimpHeight = 69;

			// -203 is half the bar's width
			Rect barBackDims = new Rect((AspectRatioFixer.SimulatedWidth * 0.5f) - barWidth * 0.5f, AspectRatioFixer.SimulatedHeight * 0.95f, barWidth, barHeight);
			//Rect borderDims = new Rect(AspectRatioFixer.SimulatedWidth * 0.5f, barBackDims.y, barBackDims.width, barHeight);

			Rect leftCapDims = new Rect(barBackDims.x, barBackDims.y, endCapWidth, barHeight);
			Rect middleBackDims = new Rect(leftCapDims.x + leftCapDims.width, leftCapDims.y, barBackDims.width - (endCapWidth * 2), barHeight);
			Rect rightCapBackDims = new Rect(middleBackDims.x + middleBackDims.width, leftCapDims.y, endCapWidth, barHeight);

			Rect borderLeftCapDims = new Rect(barBackDims.x - 2, barBackDims.y-1, barBorderEndCapWidth, barBorderHeight);
			Rect borderMiddleBackDims = new Rect(leftCapDims.x + leftCapDims.width, leftCapDims.y - 1, barBorderWidth - (endCapWidth * 2), barBorderHeight);
			Rect borderRightCapBackDims = new Rect(middleBackDims.x + middleBackDims.width, leftCapDims.y-1, barBorderEndCapWidth, barBorderHeight);


			float fillWidth = (barBackDims.width * avgProg) - (endCapWidth * 2);
			fillWidth = fillWidth > 0 ? fillWidth : 1;
			Rect fillDims = new Rect(leftCapDims.x + leftCapDims.width, barBackDims.y, fillWidth, barHeight);
			Rect rightCapDims = new Rect(fillDims.x + (fillDims.width), barBackDims.y, endCapWidth, barHeight);
			// -50 is half the blimp's width 
			Rect blimpDims = new Rect(rightCapDims.x - blimpWidth * 0.5f, barBackDims.y - (blimpHeight*0.5f), blimpWidth, blimpHeight);

			GUI.Label(leftCapDims, "", loadBarBack_LeftCap);
			GUI.Label(middleBackDims, "", loadBarBack_Middle);
			GUI.Label(rightCapBackDims, "", loadBarBack_RightCap);

			GUI.Label(rightCapDims, "", loadBarFill_RightCap);
			GUI.Label(leftCapDims, "", loadBarFill_LeftCap);
			GUI.Label(fillDims, "", loadBarFill);

			GUI.Label(borderLeftCapDims, "", loadBarBorder_LeftCap);
			GUI.Label(borderMiddleBackDims, "", loadBarBorder_Middle);
			GUI.Label(borderRightCapBackDims, "", loadBarBorder_RightCap);

			GUI.Label(blimpDims, "", loadBarTravellingIcon);
			*/
		}
	}

	/// <summary>
	///  loads an assetBundle from the url. It assumes the bundles is a .unity3d, and will try to append that to the url if it is not found. 
	/// </summary>
	public void LoadBundleAsync(string urlFromBundles) {
		StartCoroutine(loadBundleAsync(urlFromBundles,true));
	}

	/// <summary>
	///  loads an assetBundle from the url. It assumes the bundles is a .unity3d, and will try to append that to the url if it is not found. 
	/// </summary>
	public void LoadBundleAsync(string urlFromBundles, bool enforceUnityBundle) {
		StartCoroutine(loadBundleAsync(urlFromBundles, enforceUnityBundle));
	}

	// Call LoadBundleAsync instead.
	// this way, StartCoroutine() is handled by that function, and if a class has a this.StopAllCoroutines()
	// the load will not get stopped (I'm looking at you SceneLoader)
	private IEnumerator loadBundleAsync(string urlFromBundles, bool enforceUnityBundle) {
		if(!blockDownloads) {
			AssetBundle assetBundle;
			if(enforceUnityBundle) {
				properPath(ref urlFromBundles);
			}
			if(!bundleDict.ContainsKey(urlFromBundles) && !loadingDict.ContainsKey(urlFromBundles)) {
				WWW download = null;
				string fullURL = GetFullURL(urlFromBundles);
				
				//if(TelosDebug.ShowTTLogs)
					Debug.Log("FullURL of bundle: " + fullURL);
				download = WWW.LoadFromCacheOrDownload(fullURL, version);
				//download = new WWW(fullURL);
				//Debug.Log("Trying to download " + urlFromBundles + " bundle from " + fullURL);
				
				if(download != null) {
					loadingDict.Add(urlFromBundles, download);
					while(!loadingDict[urlFromBundles].isDone) {
						yield return 0;
					}
					yield return loadingDict[urlFromBundles];
					if(loadingDict[urlFromBundles].error != null) {
						Debug.LogError("Error loading bundle from " + fullURL + "\n   " + loadingDict[urlFromBundles].error);
						loadingDict[urlFromBundles].Dispose();
						loadingDict.Remove(urlFromBundles);
						download.Dispose();
						yield break;
					}
					

					while(loadingDict[urlFromBundles] != null && loadingDict[urlFromBundles].progress < 1) {
						yield return loadingDict[urlFromBundles];
					}
					yield return new WaitForSeconds(0.3f);
					assetBundle = loadingDict[urlFromBundles].assetBundle;
					//Debug.Log("finished loading bundle from " + fullURL);

					if(assetBundle != null) {
						bundleDict.Add(urlFromBundles, assetBundle);
					}
					loadingDict[urlFromBundles].Dispose();
					loadingDict.Remove(urlFromBundles);
					download.Dispose();

				}
				else {
					Debug.LogError(urlFromBundles + " download failed from " + fullURL);
				}
			}
			else if(bundleDict.ContainsKey(urlFromBundles)) {
				//assetBundle = bundleDict[urlFromBundles];
			}
			else {

			}
			yield return 1;
		}
	}

	// Use fancy logic to get the actual path
	// assumes the partialURL begins from inside the AssetBundles folder
	// if you need the .unity3d extension, provide it with the partialURL or after you've retrieved the full.
	public string GetFullURL(string partialURL) {
		string fullURL = partialURL;
		//fullURL = SmartFox2Manager.CDNURL + "/WebPlayer/AssetBundles/" + partialURL + SmartFox2Manager.CDNPostfix;
		// assumes that if it starts with either, then it's a fully valid url that we should follow till the ends of the earth!
		//if(urlFromBundles.IndexOf("file://") == 0 || urlFromBundles.IndexOf("http://") == 0)
		//	download = new WWW(urlFromBundles);
		if(Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer) {
			//fullURL = "http://" + SmartFox2Manager.CDNURL + "/WebPlayer/AssetBundles/" + partialURL + SmartFox2Manager.CDNPostfix;
			//fullURL = "https://ooka-web-asset-bundles.s3.amazonaws.com/beta/" + partialURL;

			//Debug.Log("used webPlayer fullURL");
		}
		else if(Application.platform == RuntimePlatform.OSXEditor){
			fullURL = "file://" + Application.dataPath + "/../AssetBundles/" + partialURL;
		}
		else if(Application.platform == RuntimePlatform.WindowsEditor) {
			fullURL = "file://" + Application.dataPath + "/../AssetBundles/" + partialURL;
			//fullURL = "https://ooka-web-asset-bundles.s3.amazonaws.com/beta/" + partialURL;
			//Debug.Log("used editor fullURL");
		}
		else if(Application.platform == RuntimePlatform.OSXPlayer) {
			//fullURL = "http://" + SmartFox2Manager.CDNURL + "/WebPlayer/iOSAssetBundles/" + partialURL + SmartFox2Manager.CDNPostfix;
			//Debug.Log("used osxPlayer fullURL");
		}
		else if(Application.platform == RuntimePlatform.WindowsPlayer) {
			fullURL = "file://" + Application.dataPath + "/../AssetBundles/" + partialURL;
			//Debug.Log("used windowsPlayer fullURL");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			fullURL = "https://ooka-ios-asset-bundles.s3.amazonaws.com/beta/" + partialURL;

			string tempString = fullURL;
			tempString = tempString.Replace(" ", "%20");
			fullURL = tempString;
			tempString = null;
		}
		else { //if(urlFromBundles.IndexOf("file://") == 0 || urlFromBundles.IndexOf("http://") == 0)
			fullURL = partialURL;
			//Debug.Log("used partialURL as fullURL");
		}
		return fullURL;
	}

	/// <summary>
	/// Tries to return assetBundle from the provided url.
	/// If enforceUnityBundle is false, YOU must include proper extension in url. Otherwise it will fail.
	/// Use for cases where bundles are .asset or other non .unity3d packages.
	/// </summary>
	public AssetBundle GetBundle(string urlFromBundles, bool enforceUnityBundle) {
		AssetBundle bundle = null;
		if(enforceUnityBundle) {
			properPath(ref urlFromBundles);
		}
		// Debug.Log(Application.loadedLevelName + " <-- " + urlFromBundles);
		if(bundleDict.ContainsKey(urlFromBundles)) {
			bundle = bundleDict[urlFromBundles];
		}
		// good for grabbing the 'sync' loading bundles, which usually take an extra tick to get noticed as ready for the bundleDict
		else if(loadingDict.ContainsKey(urlFromBundles) && loadingDict[urlFromBundles].progress == 1) {
			//Debug.Log("AAAaaaw, it's done enough. Grab it!");
			bundle = loadingDict[urlFromBundles].assetBundle;
		}
		else {
			Debug.LogWarning("Get " + urlFromBundles + " bundle failed. Bundle wasn't loaded");
		}
		return bundle;
	}

	/// <summary>
	/// tries to return the Asset bundle at the provided url. Assumes the url should end with .unity3d
	/// </summary>
	public AssetBundle GetBundle(string urlFromBundles){
		return GetBundle(urlFromBundles, true);
	}

	public float LoadProgress(string urlFromBundles) {
		float prog = 0f;
		properPath(ref urlFromBundles);
		if(bundleDict.ContainsKey(urlFromBundles)) {
			prog = 1f;
			//Debug.Log("found " + urlFromBundles +" in dictionary of loaded bundles");
		}
		else if(loadingDict.ContainsKey(urlFromBundles)) {
			prog = loadingDict[urlFromBundles].progress;
		}
		else {
			//Debug.Log("did not find " + urlFromBundles + " in either loading or loaded dictionary");
		}
		//Debug.Log(urlFromBundles + " loading... " + (prog*100f)+"%");
		return prog;
	}

	///<summary>
	/// simply checks if any bundles are currently downloading. If there are, then false is returned.
	/// </summary>
	public bool LoadingComplete() {
		return loadingDict.Count == 0 || onlyExcludedFromChecksLeft();
	}

	private bool onlyExcludedFromChecksLeft() {
		bool result = true;

		string[] excludes = BundlesPerScene.BundlesExcludedFromLoadCompleteCheck;
		foreach(string bundleName in loadingDict.Keys) {
			bool excludedFound = false;
			for(int i = 0; i < excludes.Length; i++) {
				if(excludes[i] == bundleName) {
					excludedFound = true;
				}
			}
			// the bundleName didn't match any of our exclude list. So there's still some needing loading
			if(!excludedFound) {
				result = false;
				break;
			}
		}

		return result;
	}
	public void UnloadBundle(string urlFromBundles, bool unloadAll) {
		properPath(ref urlFromBundles);
		if(bundleDict.ContainsKey(urlFromBundles) && bundleDict[urlFromBundles] != null) {
			bundleDict[urlFromBundles].Unload(unloadAll);
		}
	}

	// assumes to unload all
	public void UnloadBundle(string urlFromBundles) {
		UnloadBundle(urlFromBundles, true);
	}

	public void UnloadAllNonRequired() {
		UnloadAllNonRequired(Application.loadedLevelName);
	}

	// unloads any bundles not in the 'required' list for the current scene
	public void UnloadAllNonRequired(string sceneName) {
		//List<KeyValuePair<string, AssetBundle>> theList = new List<KeyValuePair<string, AssetBundle>>(bundleDict);
		List<string> keysToDelete = new List<string>();
		foreach (KeyValuePair<string, AssetBundle> kvp in bundleDict)
		{
		//for(int i = 0; i < theList.Count; i++) {
			bool matchFound = false;
			string[] bundsForScene = BundlesPerScene.GetBundlesForScene(sceneName);
			for(int j = 0; j < bundsForScene.Length; j++ ) {
				if(kvp.Key == bundsForScene[j]) {
					matchFound = true;
				}
			}
			// we found a match. It's a loaded bundle. Now check if it's one we're supposed to leave loaded
			// only worth checking if we're not already keeping it 
			if(!matchFound) {
				bundsForScene = BundlesPerScene.BundlesExcludedFromUnload;
				
				for(int j = 0; j < bundsForScene.Length; j++ ) {
					// found it in the exclude list, so we pretend we never saw it in the required list for this level too
					if(kvp.Key == bundsForScene[j] || kvp.Key.Contains(".assetbundle")) {
						matchFound = true;
					}
				}
			}
			//
			if(!matchFound) {
				UnloadBundle(kvp.Key);
				keysToDelete.Add (kvp.Key);
			}
		}
		foreach (string s in keysToDelete)
		{
			bundleDict.Remove (s);
		}
	}

	public void UnloadAllBundles() {
		//List<KeyValuePair<string, AssetBundle>> theList = new List<KeyValuePair<string, AssetBundle>>(bundleDict);
		//for(int i = 0; i < theList.Count; i++) {
		foreach(KeyValuePair<string, AssetBundle> kvp in bundleDict)
		{
			UnloadBundle(kvp.Key);
		}
		bundleDict.Clear ();
	}

	private void properPath(ref string url) {
		if(!url.EndsWith(".unity3d")) {
			url += ".unity3d";
		}
		if(url.StartsWith("/")) {
			url = url.Remove(0, 1);
		}
	}

	public BundleLoadStatus GetBundleStatus(string urlFromBundles) {
		return GetBundleStatus(urlFromBundles, true);
	}

	public BundleLoadStatus GetBundleStatus(string urlFromBundles, bool enforceUnityBundle) {
		if(enforceUnityBundle) {
			properPath(ref urlFromBundles);
		}
		BundleLoadStatus status = BundleLoadStatus.UNKNOWN;
		if(bundleDict.ContainsKey(urlFromBundles)) {
			if(bundleDict[urlFromBundles] == null) {
				status = BundleLoadStatus.NOTLOADED;
				bundleDict.Remove(urlFromBundles);
				Debug.Log(urlFromBundles + " was not loaded.");
			}
			else{
				status = BundleLoadStatus.LOADED;
			}
		}
		else if(loadingDict.ContainsKey(urlFromBundles)) {
			status = BundleLoadStatus.LOADING;
		}

		return status;
	}

	bool blockDownloads = false;
	void OnApplicationQuit() 
	{
		blockDownloads = true;
		//Debug.Log("AssetBundleManger cleaned up.");
		this.StopAllCoroutines();
		if (loadingDict != null)
		{
			if (loadingDict.Values != null)
			{
				foreach(WWW www in loadingDict.Values) 
				{
					www.Dispose();
				}
			}
		}
	
		loadingDict.Clear();

		if (bundleDict != null)
		{
			if (bundleDict.Values != null)
			{
				foreach(AssetBundle ab in bundleDict.Values) 
				{
					if(ab != null) ab.Unload(true);
				}
			}
		}
		bundleDict.Clear();
		StopAllCoroutines();
	}
}