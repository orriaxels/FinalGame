using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamage : MonoBehaviour {

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
			if(col.gameObject.GetComponent<PlayerController>().hasDoubleDamage == false)
			{
				col.gameObject.GetComponent<PlayerController>().hasDoubleDamage = true;
				Destroy(this.gameObject);
			}
		}
	}
}
