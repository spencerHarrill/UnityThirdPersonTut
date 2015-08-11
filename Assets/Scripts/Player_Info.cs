using UnityEngine;
using System.Collections;

public class Player_Info : MonoBehaviour {

	public Camera playerCamera;
	//public CameraScript camScript;
	public PlayerMovement moveScript;
	public Transform spawnPoint;
	// Use this for initialization
	Animator anim;
	void Start () {
		//camScript = gameObject.GetComponent<CameraScript>();
		moveScript = gameObject.GetComponent<PlayerMovement>();

	}
	void Update (){
		anim = gameObject.GetComponentInChildren<Animator>();
		anim.SetFloat ("Speed",moveScript.velocity.magnitude );
	}
	
	// Update is called once per frame
	public void Kill () {
		transform.position = spawnPoint.position;
		moveScript.Kill ();
	}
}
