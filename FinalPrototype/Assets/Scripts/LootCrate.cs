using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : MonoBehaviour {

	public GameObject explosion; 
	public GameObject lootIndicator;
	public List<GameObject> loot;	
	private int health = 100;
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
		{
			Destroy(this.gameObject);
			
			// creating crate explosion
			Vector3 posExplosion = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			GameObject expl = Instantiate(explosion, posExplosion, transform.rotation);
			Destroy(expl, 2.0f);	
			
			// Loot Indicator
			Vector3 posIndicator = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
			Instantiate(lootIndicator, posIndicator, transform.rotation);

			// Loot
			Vector3 posLoot = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
			Instantiate(loot[0], posLoot, transform.rotation);
		}
	}

	public void takeDamage(int damage)
	{
		Debug.Log("LootCrate Health: " + health);
		health -= damage;
	}
}
