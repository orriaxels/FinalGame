using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootIndicator : MonoBehaviour {

	
	public bool isShield;
	public bool isDoubleDamage;
	public bool isHealthKit;
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
			if(isShield)
			{
				if(col.gameObject.GetComponent<PlayerController>().hasShield == false)
				{
					Destroy(this.gameObject);
				}
			}

			if(isDoubleDamage)
			{
				if(col.gameObject.GetComponent<PlayerController>().hasDoubleDamage == false)
				{
					Destroy(this.gameObject);
				}
			}


			if(isHealthKit)
			{
				if(col.gameObject.GetComponent<PlayerController>().hasHealthKit == false)
				{
					Destroy(this.gameObject);
				}
			}
			
		}
	}
}
