using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetManager : Singleton<AssetManager> {

	#region AssetKey
	public class AssetKey {
		private AssetKey() { }

		public AssetKey(string name, AssetType type) {
			Name = name;
			Type = type;
		}

		string mName;
		public string Name {
			get {
				return mName;
			}
			set {
				mName = value;
			}
		}
		AssetType mType;
		public AssetType Type {
			get {
				return mType;
			}
			set {
				mType = value;
			}
		}
	}
	#endregion AssetKey

	#region AssetDetails
	public class AssetDetails {
		private AssetDetails() { }
		public AssetDetails(string realName, string bundle) {
			mRealName = realName;
			mBundle = bundle;
		}

		private string mRealName;
		public string RealName {
			get {
				return mRealName;
			}
		}

		private string mBundle;
		public string Bundle {
			get {
				return mBundle;
			}
		}
	}
	#endregion AssetDetails

	#region AssetInfo
	public class AssetInfo {
		private AssetInfo(){}
		public AssetInfo(AssetKey key, AssetDetails details){
			mKey = key;
			mDetails = details;
		}

		#region Get AssetKey values
		AssetKey mKey;
		public AssetKey Key {
			get {
				return mKey;
			}
		}

		public string Name {
			get {
				return mKey.Name;
			}
		}

		public AssetType Type {
			get {
				return mKey.Type;
			}
		}
		#endregion

		#region Get AssetDetail values
		AssetDetails mDetails;
		public AssetDetails Details {
			get {
				return mDetails;
			}
		}

		public string RealName {
			get {
				return mDetails.RealName;
			}
		}

		public string Bundle {
			get {
				return mDetails.Bundle;
			}
		}
		#endregion Get AssetDetail values
	}
	#endregion AssetInfo

	public enum AssetType {
		AUDIOCLIP, TEX2D, SPRITE, SPRITESHEET, MODEL, PREFAB, ANIMATION
	}
	Dictionary<AssetKey, AssetDetails> mDictAssets;

	void Awake() {
		mDictAssets = new Dictionary<AssetKey, AssetDetails>();
	}

	/// <summary>
	/// Searches the dictionary of assets for one matching the provided name and type. Returns null if it can not.
	/// </summary>
	/// <param name="assetName"></param>
	/// <param name="assetType"></param>
	/// <returns></returns>
	public AssetInfo FindAsset(string assetName, AssetType assetType) {
		AssetInfo result = null;
		AssetKey key = new AssetKey(assetName, assetType);
		if(mDictAssets.ContainsKey(key)) {
			result = new AssetInfo(key, mDictAssets[key]);
		}

		return result;
	}

	public AudioClip GetClip(string assetName) {
		AssetInfo info = FindAsset(assetName, AssetType.AUDIOCLIP);
		AudioClip ac = null;
		if(info != null) {
			if(!string.IsNullOrEmpty( info.Bundle)) {
				AssetBundleManager.Instance.LoadBundleAsync(info.Bundle);
			}
		}

		return ac;
	}
}
