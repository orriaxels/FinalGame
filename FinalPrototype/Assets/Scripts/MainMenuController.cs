using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character
	private Player player; // The Rewired Player
	public EventSystem eventSystem;
	public GameObject selectedObject;
	public GameObject backButton;

	private bool buttonSelected;

	int motorIndex = 0; // the first motor
	float motorLevel = 0.1f; // full motor speed
	float duration = 0.1f;
	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(playerId);
	}
	
	// Update is called once per frame
    void Update () 
    {
        if (player.GetAxis ("UIVertical") != 0) 
        {
			player.SetVibration(motorIndex, motorLevel, duration);
        }

		if(player.GetButtonDown("UISubmit"))
		{
			Debug.Log("submit, should vibrate");
			player.SetVibration(motorIndex, motorLevel, duration);
		}
    }
}
