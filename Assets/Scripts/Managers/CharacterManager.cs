using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager> {

	List<Character> m_lstAllCharacters;
	List<Player> m_lstAllPlayer;

	void Awake() {
		if(_instance != null) {
			DestroyImmediate(this);
		}
		else {
			DontDestroyOnLoad(this);
			m_lstAllCharacters = new List<Character>();
			m_lstAllPlayer = new List<Player>();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddCharacter(Character newChar) {
		m_lstAllCharacters.Add(newChar);
	}

	public void RemoveCharacter(Character chr) {
		m_lstAllCharacters.Remove(chr);
	}

	public void AddPlayer(Player newPlayer) {
		m_lstAllPlayer.Add(newPlayer);
	}

	public void RemovePlayer(Player plyr) {
		m_lstAllPlayer.Remove(plyr);
	}
}
