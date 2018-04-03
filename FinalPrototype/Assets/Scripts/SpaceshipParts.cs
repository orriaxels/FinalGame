using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipParts : MonoBehaviour {

	private GameController gameController;
	// Use this for initialization
	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			if(gameObject.name == "MainBoard") {
				gameController.hasMotherBoard = true;
			} 
			else if(gameObject.name == "Cog") {
				gameController.hasCog = true;
			}
			else if(gameObject.name == "Crystals"){
				gameController.hasCrystal = true;
			}
			else if(gameObject.name == "Gasoline") {
				gameController.hasGasoline = true;
			}
			else if (gameObject.name == "Wrench") {
				gameController.hasWrench = true;
			}

			Destroy(this.gameObject);
		}
	}
}
