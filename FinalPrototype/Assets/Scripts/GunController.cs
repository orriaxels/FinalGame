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
		if (isFiring && !overHeat)
		{
			
			timer -= Time.deltaTime;

			if(timer <= 0)
			{
				Bullet newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as Bullet;
				newBullet.speed = bulletSpeed;
				overHeatTimer -= 0.25f;
				timer = cooldown;
				if(transform.parent.gameObject.GetComponent<PlayerController>().usingDoubleDamage)
				{
					newBullet.damage = 50;
				}
			}
		} 
		else 
		{
			overHeatTimer += Time.deltaTime * 1.5f;
			if (overHeatTimer > overHeatCoolDown)
				overHeatTimer = overHeatCoolDown;
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
