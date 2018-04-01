using System.Collections;
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
            Instantiate(impactEffect, transform.position, transform.rotation);
        }


        if (col.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
            if (!enemyBullet)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
        }

        if (col.gameObject.tag == "Turret" && !enemyBullet)
        {
            col.gameObject.GetComponent<TurretController>().TakeDamage(damage);
            Destroy(this.gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }

}
