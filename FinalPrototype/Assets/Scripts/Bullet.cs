﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
	public bool hit;
    public bool enemyBullet = false;

    public float damage;
	public float timeAlive;
    public GameObject impactEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        Debug.Log("Damage: " + damage);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

		timeAlive += Time.deltaTime;
		if (timeAlive >= 2)
			Destroy (gameObject);
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && enemyBullet)
        {
            col.gameObject.GetComponent<PlayerController>().takeDamage(damage);
            Destroy(this.gameObject);
            bulletImpact();
        }

        if (col.gameObject.name == "Shield" && enemyBullet)
        {
            Destroy(this.gameObject);
            bulletImpact();
        }


        if (col.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
            if (!enemyBullet)
            {            
                bulletImpact();
            }
        }

        if (col.gameObject.tag == "Turret" && !enemyBullet)
        {
            col.gameObject.GetComponent<TurretController>().TakeDamage(damage);
            Destroy(this.gameObject);
            bulletImpact();
        }

        if(col.gameObject.tag == "LootCrate")
        {
            col.gameObject.GetComponent<LootCrate>().takeDamage(20);
            Destroy(this.gameObject);
            bulletImpact();
        }
    }

    private void bulletImpact() 
    {
        GameObject bulletImpact = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(bulletImpact, 1.0f);
    }

}
