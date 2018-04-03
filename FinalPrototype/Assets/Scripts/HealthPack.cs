using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

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
			if(col.gameObject.GetComponent<PlayerController>().hasHealthKit == false)
			{
				col.gameObject.GetComponent<PlayerController>().hasHealthKit = true;
				Destroy(this.gameObject);
			}
		}
	}
}
