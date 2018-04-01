using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour
{
    private Animator anim;

    public float LeftHandWeight = 1;
    public Transform LeftHandTarget;

    public float RightHandWeight = 1;
    public Transform RightHandTarget;

    public Transform weapon;
    public Vector3 lookPos;

	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnAnimatorIK()
    {
//		if (gameObject != null) {
			anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
			anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.position);
			anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
			anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTarget.rotation);
			
			anim.SetIKPositionWeight(AvatarIKGoal.RightHand, RightHandWeight);
			anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
			anim.SetIKRotationWeight(AvatarIKGoal.RightHand, RightHandWeight);
			anim.SetIKRotation(AvatarIKGoal.RightHand, RightHandTarget.rotation);		
//		}
    }
}
