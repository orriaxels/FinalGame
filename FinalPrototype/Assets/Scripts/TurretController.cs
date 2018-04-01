using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

	public Transform redPlayer;
	public Transform bluePlayer;
	public Transform firePoint;
    public float health;

	public bool startToAim;
    public float startToAimDistance;

	public Bullet bullet;
	public float bulletSpeed;

	public float cooldown;
	public float timer;

	public float timeAlive;

	private EnemiesCollider enemiesCollider;

	// Use this for initialization
	void Start () 
	{
		enemiesCollider = GameObject.FindObjectOfType<EnemiesCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distanceToRed = redPlayer.position - this.transform.position;
		Vector3 distanceToBlue = bluePlayer.position - this.transform.position;
		Vector3 direction;

		

		if (distanceToRed.magnitude <= startToAimDistance || distanceToBlue.magnitude <= startToAimDistance) 
			startToAim = true;
		else
			startToAim = false;

		if (distanceToRed.magnitude < distanceToBlue.magnitude) 
		{
			direction = distanceToRed;
		} 
		else 
		{
			direction = distanceToBlue;
		}

		if(startToAim)
		{			
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
				Quaternion.LookRotation(direction), 0.1f);

            timer -= Time.deltaTime;
		    if (timer <= 0)
		    {
		        Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
		        newBullet.speed = bulletSpeed;
		        timer = cooldown;
		    }

            //			if (direction.magnitude > stopToChaseDistance) 
            //			{				
            //				this.transform.Translate (0, 0, 0.05f);
            //			}
        }

		if (health <= 0) 
		{
			Destroy (gameObject);
		}

//		Debug.Log (health);
	}

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
