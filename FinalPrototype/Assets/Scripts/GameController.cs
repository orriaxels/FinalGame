using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityStandardAssets.ImageEffects;


public class GameController : MonoBehaviour {

	public bool hasMotherBoard;
	public bool hasGasoline;
	public bool hasCog;
	public bool hasCrystal;
	public bool hasWrench;

	public bool canLeave;

	public Image wrench;
	public Image motherBoard;
	public Image cog;
	public Image crystal;
	public Image gasoline;
	
	public GameObject redPlayer;
	public GameObject bluePlayer;

	// GameObjects
	public GameObject CameraRig;
	public Canvas CameraRigCanvas;
	public GameObject PauseMenu;
	public Camera mainCamera;

	// PauseMenu
	public bool paused = false;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hasCog)	
			changeAlpha(cog, 1);
		if(hasCrystal)
			changeAlpha(crystal, 1);
		if(hasGasoline)
			changeAlpha(gasoline, 1);
		if(hasMotherBoard)
			changeAlpha(motherBoard, 1);
		if(hasWrench)
			changeAlpha(wrench, 1);

		if(hasCog && hasCrystal && hasGasoline && hasMotherBoard && hasWrench)
			canLeave = true;


		if(paused)
		{
			Time.timeScale = 0;
			CameraRigCanvas.enabled = false;
			PauseMenu.SetActive(true);
			mainCamera.GetComponent<Blur>().enabled = true;

			redPlayer.GetComponent<PlayerController>().paused = true;
			redPlayer.GetComponent<PlayerController>().setActionId(2);

			bluePlayer.GetComponent<PlayerController>().paused = true;
			bluePlayer.GetComponent<PlayerController>().setActionId(2);
		}
		else if(!paused)
		{
			Time.timeScale = 1;
			CameraRigCanvas.enabled = true;
			PauseMenu.SetActive(false);
			mainCamera.GetComponent<Blur>().enabled = false;
			
			redPlayer.GetComponent<PlayerController>().paused = false;
			redPlayer.GetComponent<PlayerController>().setPlayerOriginalId();

			bluePlayer.GetComponent<PlayerController>().paused = false;
			bluePlayer.GetComponent<PlayerController>().setPlayerOriginalId();
		}
	}

	private void FixedUpdate()
	{
	}

	private void changeAlpha(Image image, float value)
	{
		Color c = image.color;
		c.a = value;
		image.color = c;
	}

	public void resumeGame()
	{
		paused = false;
	}
}
