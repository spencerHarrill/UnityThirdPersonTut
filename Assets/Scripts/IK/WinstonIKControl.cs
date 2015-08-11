using UnityEngine;
using System;
using System.Collections;
[RequireComponent(typeof(Animator))]
public class WinstonIKControl : MonoBehaviour {
	protected Animator animator;	
	public bool ikActive = false;
    public LayerMask ikMask;
    public Transform desiredRightFootLoc;
    public Transform desiredLeftFootLoc;
	public Transform rightFootCheckLoc = null;
	public Transform leftFootCheckLoc = null;
	// Use this for initialization
	void Start () {
		animator= GetComponent<Animator>();
	}
    // Update is called once per frame
    void Update()
    {
        RaycastHit rHit;
        RaycastHit lHit;
        Ray rightFoot = new Ray(rightFootCheckLoc.position, -Vector3.up);
        Ray leftFoot = new Ray(leftFootCheckLoc.position, -Vector3.up);

        //Debug.DrawRay(rightFootCheckLoc.position, -Vector3.up);
        //if (Physics.Raycast(rightFoot, out rHit, 2f))
        //{
        //    desiredRightFootLoc.position = rHit.point;

        //}
    }
    void OnAnimatorIK()
	{
		if(ikActive){
		if(leftFootCheckLoc != null){
				animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,1);
				animator.SetIKPosition (AvatarIKGoal.LeftFoot,desiredLeftFootLoc.position);
				//animator.SetIKRotation(AvatarIKGoal.LeftFoot,leftFootLoc.rotation);
			}
			if(rightFootCheckLoc != null){
				animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
				animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1);
				animator.SetIKPosition (AvatarIKGoal.RightFoot,desiredRightFootLoc.position);
				//animator.SetIKRotation(AvatarIKGoal.RightFoot,rightFootLoc.rotation);
			}
		}else{
			animator.SetIKPositionWeight (AvatarIKGoal.LeftFoot,0);
			animator.SetIKPositionWeight (AvatarIKGoal.RightFoot,0);
		}
	}

}
