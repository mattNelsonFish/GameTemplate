using UnityEngine;
using System.Collections;

public class BundlesPerScene{

	// TODO: change to sceneName + state, so we can decide whether to load certain assets based on the state of the game/scene.
	public static string[] GetBundlesForScene(string sceneName){
		string[] result;

		switch(sceneName){
			default:
				result = new string[] { };
				break;
		}

		return result;
	}

	static string[] bundlesExcludedFromLoadCompleteCheck;
	public static string[] BundlesExcludedFromLoadCompleteCheck {
		get{
			if(bundlesExcludedFromLoadCompleteCheck == null) {
				bundlesExcludedFromLoadCompleteCheck = new string[]{/*fill me with bundle names*/};
			}
			return bundlesExcludedFromLoadCompleteCheck;
		}
	}

	static string[] bundlesExcludedFromUnload;
	public static string[] BundlesExcludedFromUnload {
		get {
			if(bundlesExcludedFromUnload == null) {
				bundlesExcludedFromUnload = new string[] { };
			}
			return bundlesExcludedFromUnload;
		}
	}
}
