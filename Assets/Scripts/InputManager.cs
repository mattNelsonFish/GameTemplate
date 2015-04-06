using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// menu stuffs
		if(GameManager.Instance.IsPaused) {

		}
		// normal input
		else {
			if(Input.GetKey(KeyCode.W)) {
				// event to move player
			}
			if(Input.GetKey(KeyCode.A)) {
				// event to move player
			}
			if(Input.GetKey(KeyCode.S)) {
				// event to move player
			}
			if(Input.GetKey(KeyCode.D)) {
				// event to move player
			}
		}
	}
	/*
		Normal keys: “a”, “b”, “c” …
		Number keys: “1”, “2”, “3”, …
		Arrow keys: “up”, “down”, “left”, “right”
		Keypad keys: “[1]”, “[2]”, “[3]”, “[+]”, “[equals]”
		Modifier keys: “right shift”, “left shift”, “right ctrl”, “left ctrl”, “right alt”, “left alt”, “right cmd”, “left cmd”
		Mouse Buttons: “mouse 0”, “mouse 1”, “mouse 2”, …
		Joystick Buttons (from any joystick): “joystick button 0”, “joystick button 1”, “joystick button 2”, …
		Joystick Buttons (from a specific joystick): “joystick 1 button 0”, “joystick 1 button 1”, “joystick 2 button 0”, …
		Special keys: “backspace”, “tab”, “return”, “escape”, “space”, “delete”, “enter”, “insert”, “home”, “end”, “page up”, “page down”
		Function keys: “f1”, “f2”, “f3”, …
	 * */
}
