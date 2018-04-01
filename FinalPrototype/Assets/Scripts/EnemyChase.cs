using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour {

	public Transform redPlayer;
	public Transform bluePlayer;

	List<GameObject> bulletObjects;

	public bool startToChase;

	public float health;
	public float stopToChaseDistance;
    public float startToChaseDistance;

	private EnemiesCollider enemiesCollider;
	private Bullet bullet;

	// Use this for initialization
	void Start () 
	{
		enemiesCollider = GameObject.FindObjectOfType<EnemiesCollider> ();


		health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distanceToRed = redPlayer.position - this.transform.position;
		Vector3 distanceToBlue = bluePlayer.position - this.transform.position;
		Vector3 direction;


//		GameObject blast = GameObject.FindGameObjectWithTag("LaserBlast");
//
//		if(blast != null)
//			bullet = GameObject.FindObjectOfType<Bullet> ();
//		
//
//		if(bullet != null)
//			Debug.Log ("fdsafdsa" + bullet.hit);

	    if (distanceToRed.magnitude <= startToChaseDistance || distanceToBlue.magnitude <= startToChaseDistance) 
		{
			startToChase = true;
		}

		if (distanceToRed.magnitude < distanceToBlue.magnitude) 
		{
			direction = distanceToRed;
		} 
		else 
		{
			direction = distanceToBlue;
		
		}
		
		if(startToChase)
		{			
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
										Quaternion.LookRotation(direction), 0.1f);

			if (direction.magnitude > stopToChaseDistance) 
			{				
				this.transform.Translate (0, 0, 0.05f);
			}
		}

		if (health <= 0) 
		{
			Destroy (gameObject);
		}

		Debug.Log (health);
	}

	void OnCollisionEnter(Collision col)
	{		

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "LaserBlast") 
		{
			health -= 25;
		    if (!col.gameObject.GetComponent<Bullet>().enemyBullet)
		    {
		        Instantiate(col.gameObject.GetComponent<Bullet>().impactEffect, transform.position, transform.rotation);
            }
			Destroy(col.gameObject);
		}

	}
}
