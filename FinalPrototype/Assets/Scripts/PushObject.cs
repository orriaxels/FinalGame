using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Rigidbody))]
public class PushObject : MonoBehaviour {

    public AudioClip soundClip;
	public int playerId;
    public float ObMass = 300;
    public float PushAtMass = 100;
    public float PushingTime;
    public float ForceToPush;
    public float vel;
	public bool touchingCube = false;

    Rigidbody rb;
    AudioSource AudioPlayer;
    Vector3 dir;
	Vector3 lastPos ;

    float _PushingTime = 0;
	private Player player; 
    

	void Awake() {
		player = ReInput.players.GetPlayer(playerId);
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
        if (rb == null) return;

        // AudioPlayer = GetComponent<AudioSource>();
        // if (soundClip != null)
        // {           
        //     AudioPlayer.clip = soundClip;
        //     AudioPlayer.Stop();
        // }
        // AudioPlayer.volume = 0;
        // AudioPlayer.pitch = 0.5f;
        rb.mass = ObMass;
		
	}

	bool IsMoving()
    {
        if (rb.velocity.magnitude > 0.06f)
        {
            return true;
        }
        return false;

    }
	
	// Update is called once per frame
	void Update () {
		 //F key to Push
        if (Input.GetKeyUp(KeyCode.F))
        {
            rb.isKinematic = false;
            // if (soundClip != null)
            // {
            //     AudioPlayer.Stop();
            // }

            // AudioPlayer.volume = 0f;
            // AudioPlayer.pitch = 0.2f;
        }

        if (rb.isKinematic==false)
        {
            _PushingTime += Time.deltaTime;
            if (_PushingTime >= PushingTime)
            {
                _PushingTime = PushingTime;
            }

            rb.mass = Mathf.Lerp(ObMass, PushAtMass, _PushingTime / PushingTime);
            rb.AddForce(dir * ForceToPush, ForceMode.Force);
        }
        else
        {
            rb.mass = ObMass;
            _PushingTime = 0;
           
        }

        if (IsMoving() == true && rb.isKinematic == false)
        {
            // if (AudioPlayer.isPlaying == false)
            // {
            //     AudioPlayer.Play();
            // }

        //    StartCoroutine( SoundChangeHigh());
        }
        else
        {
            // StartCoroutine(SoundChangeLow());
        }
    
	}

	private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player1")
        {
			// touchingCube = true;
            // Debug.Log("Player Collision");
			// Debug.Log(name);

            if (Input.GetKey(KeyCode.F))
            {
				
                rb.isKinematic = false;

                dir = collision.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
              
            }
			
        }

    }

	private void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.name == "Player1")
		{
			touchingCube = false;
			rb.isKinematic = true;
		}
	}
}
