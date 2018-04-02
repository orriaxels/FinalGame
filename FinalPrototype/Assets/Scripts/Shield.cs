using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			if(col.gameObject.GetComponent<PlayerController>().hasShield == false)
			{
				col.gameObject.GetComponent<PlayerController>().hasShield = true;
				Destroy(this.gameObject);
			}
		}
	}
}
