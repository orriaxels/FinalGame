using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCollider : MonoBehaviour {

	public bool isTriggered;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (isTriggered);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Player1" || col.gameObject.name == "Player2")
		{
			isTriggered = true;
			Debug.Log ("triggered");
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "Player1" && col.gameObject.name == "Player2")
		{
			isTriggered = false;
			Debug.Log ("triggered");
		}
	}
}
