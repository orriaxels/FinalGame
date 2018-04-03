using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	}

	private void changeAlpha(Image image, float value)
	{
		Color c = image.color;
		c.a = value;
		image.color = c;
	}
}
