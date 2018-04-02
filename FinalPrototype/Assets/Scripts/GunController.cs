using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    public bool isFiring;
	public bool overHeat;

    public List<Bullet> bullets;
	public Bullet bullet;
    
	public float bulletSpeed;
	public float cooldown;
    public float timer;
	public float overHeatCoolDown;
	public float overHeatTimer;
	public float timeAlive;

    public Transform firePoint;

	public float health = 5;
	private float startHealth = 5;
	public Image overHeatBar;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (isFiring && !overHeat) {
			if (timer == cooldown) 
			{
				Bullet newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as Bullet;
				newBullet.speed = bulletSpeed;
				if(transform.parent.gameObject.GetComponent<PlayerController>().usingDoubleDamage)
				{
					newBullet.damage = 50;
				}
			}

			overHeatTimer -= Time.deltaTime;
			timer -= Time.deltaTime;
		} 
		else 
		{
			timer = cooldown;
			overHeatTimer += Time.deltaTime * 1.5f;
			if (overHeatTimer > overHeatCoolDown)
				overHeatTimer = overHeatCoolDown;
		}

		if (timer <= 0.0f) {
			Bullet newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as Bullet;
			newBullet.speed = bulletSpeed;
			timer = 0.29f;
			if(transform.parent.gameObject.GetComponent<PlayerController>().usingDoubleDamage)
			{
				newBullet.damage = 50;
			}
		}

		if (overHeatTimer < 0) 
		{
			overHeatTimer = 0;	
			overHeat = true;
		} 
		else if (overHeatTimer > 2) 
		{
			overHeat = false;
		}

		overHeatBar.fillAmount = overHeatTimer / startHealth;
    }
}
