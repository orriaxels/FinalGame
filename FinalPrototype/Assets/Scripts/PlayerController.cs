﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

// [RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character
	public float health = 100;
    private float startHealth = 100;
	public Image healthBar;
	//public float bulletSpeed = 15.0f;
	//public GameObject bulletPrefab;

	private Player player; // The Rewired Player
	private CharacterController cc;
    private Animator anim;
	private Vector3 moveVector;
    private GunController theGun;
    private bool dash;
    public bool pushing;

	// Loot objects
	public Image shield;
	public bool hasShield = false;
	public GameObject shieldObject;

	public Image doubleDamage;
	public bool hasDoubleDamage = false;
	public bool usingDoubleDamage = false;
	public GameObject doubleDamageObject;

	private float doubleDamageTimer = 5.0f;

	private bool fire;
	public float rotateSpeed;
	public float moveSpeed; 
	public float strafeSpeed;

	private Rigidbody myRigidBody;
	private Vector3 moveInput;
	private Vector3 moveVelocity;
	private Vector3 camForward;
	private PushObject pushObject;
	Transform cam;

	float forwardAmount;
	float turnAmount;

	private PushObject pushableObj;

	void Awake() {
		// Get the Rewired Player object for this player and keep it for 
		// the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);

	    anim = GetComponent<Animator>();
		myRigidBody = GetComponent<Rigidbody>();
	    theGun = transform.Find("rifle").gameObject.GetComponent<GunController>();
	}

	void Start () {

		cam = Camera.main.transform;

		rotateSpeed = 150;

		pushObject = GameObject.FindObjectOfType<PushObject>();
		
		changeAlpha(shield, 0.2f);
		changeAlpha(doubleDamage, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {    
		GetInput();
		
		if(hasShield)
		{
			changeAlpha(shield, 1);
		}
		else
		{
			changeAlpha(shield, 0.2f);
		}

		if(hasDoubleDamage)
		{
			changeAlpha(doubleDamage, 1);
			if(usingDoubleDamage)
			{
				doubleDamageTimer -= Time.deltaTime;
				if(doubleDamageTimer <= 0)
				{
					doubleDamageTimer = 5.0f;
					hasDoubleDamage = false;
					usingDoubleDamage = false;
				}
			}
		}
		else
		{
			changeAlpha(doubleDamage, 0.2f);
		}

		Vector3 playerDirection = Vector3.right * player.GetAxisRaw("RHorizontal") + Vector3.forward * player.GetAxisRaw("RVertical");
		if(playerDirection.sqrMagnitude > 0.3f)
		{
			transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
		}
	      
	}

    private void FixedUpdate()
    {
        myRigidBody.velocity = moveVelocity;
		
		RaycastHit hit = new RaycastHit();
		
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Vector3 rayPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
		
		Debug.DrawRay(rayPos, fwd, Color.green);
        
		if(Physics.Raycast(rayPos, fwd, out hit, 1f))
		{
			
			if(hit.collider.gameObject.tag == "MovableObject") 
			{
				pushableObj = hit.transform.GetComponent<PushObject>();
				pushableObj.touchingCube = true;
			    if (player.GetButtonSinglePressHold("Use") && pushObject.GetComponent<Rigidbody>().isKinematic)
			    {
                    anim.SetBool("Pushing", true);
			        pushObject.GetComponent<Rigidbody>().isKinematic = false;
			        transform.Find("rifle").gameObject.SetActive(false);

			    }
			    else if (player.GetButtonUp("Use") && !pushObject.GetComponent<Rigidbody>().isKinematic)
			    {
			        anim.SetBool("Pushing", false);
                    pushObject.GetComponent<Rigidbody>().isKinematic = true;
			        transform.Find("rifle").gameObject.SetActive(true);
                }
			}
		}
		else
		{
			if(pushableObj != null)
			{
				pushableObj.touchingCube = false;
				pushableObj = null;
			    pushObject.GetComponent<Rigidbody>().isKinematic = true;
			    anim.SetBool("Pushing", false);
			    transform.Find("rifle").gameObject.SetActive(true);
            }
		}


		float horizontal = player.GetAxisRaw("MHorizontal");
        float vertical = player.GetAxisRaw("MVertical");

        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            moveVelocity = vertical * camForward + horizontal * cam.right;
        }
        else
        {
            moveVelocity = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (moveInput.magnitude > 1)
        {
            moveInput.Normalize();
        }

        Move(moveVelocity);

        Vector3 movement;

        bool backwards = false;



        movement = new Vector3(horizontal, 0, vertical);
        float angle = Vector3.Angle(transform.forward, movement);

        if (angle > 85)
        {
            backwards = true;
        }

        if (backwards)
        {
            movement = movement * 1f;
        }

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        myRigidBody.AddForce(movement * moveSpeed / Time.deltaTime);

    }


    private void Move(Vector3 moveVelocity)
	{
		if(moveVelocity.magnitude > 1)
		{
			moveVelocity.Normalize();
		}

		this.moveInput = moveVelocity;

		ConvertMoveInput();
		UpdateAnimator();
	}

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
    }

    private void ConvertMoveInput()
	{
		Vector3 localMove = transform.InverseTransformDirection(moveInput);
		turnAmount = localMove.x;

		forwardAmount = localMove.z;
	}

	private void UpdateAnimator()
	{
		anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
		anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
	}

	private void GetInput() 
	{
		if (player.GetButtonDown("Fire"))
		{
			theGun.isFiring = true;
		}
		else if(player.GetButtonUp("Fire"))
		{
			theGun.isFiring = false;
		}

		if(player.GetButtonDown("Up") && hasShield)
		{
			Vector3 shieldPos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
			GameObject sheildEffect = Instantiate(shieldObject, shieldPos, transform.rotation);
			sheildEffect.transform.parent = this.transform;
			Destroy(sheildEffect, 10.0f);
			hasShield = false;
		}

		if(player.GetButtonDown("Left") && hasDoubleDamage)
		{
			
			usingDoubleDamage = true;
		}
	}

	private void changeAlpha(Image image, float value)
	{
			Color c = image.color;
			c.a = value;
			image.color = c;
	}
}
