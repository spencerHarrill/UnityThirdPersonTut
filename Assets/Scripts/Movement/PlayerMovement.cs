using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	// Get info about other things on the player
	public Player_Info infoScript;

	public Vector3 initialPlayerForce;
	public float initialFrictionCoefficent;
	public float initialAirFrictionCoefficent;

	public float turnSpeed;
    public float stepOffset;

	public float mass;
	public float gravForce;

	float airFrictionCoefficent;
	float frictionCoefficent;

	public Vector3 inputForce;
	Vector3 direction;
	Vector3 friction;
    RaycastHit groundPos;
    Vector3 feetPos;
		
	CapsuleCollider charCollider;
	Rigidbody playerRigidbody;

	public Vector3 jumpForce;

	[HideInInspector]
	public Vector3 velocity;

	// Use this for initialization
	void Start () {
		// Get components
		playerRigidbody = GetComponent<Rigidbody>();
		charCollider = GetComponent<CapsuleCollider>();
		infoScript = gameObject.GetComponent<Player_Info>();
		// Save Initial forces to reset to
		inputForce = initialPlayerForce;
		frictionCoefficent = initialFrictionCoefficent;
		airFrictionCoefficent = initialAirFrictionCoefficent;
	}

	void Update() {
		GroundCheck ();
		UpdateMovement ();
	}

	// Input Variables
	float inputX;
    float inputY;
	bool jumping;
	
	void GetInput (){
	
		inputX = Input.GetAxisRaw ("Horizontal");
		inputY = Input.GetAxisRaw ("Vertical");
		jumping = Input.GetButton ("Jump");
		
		if (inputX == 0 && inputY == 0) {
			inputForce = Vector3.zero;
		} else {
			inputForce = initialPlayerForce;		
		}
	}

    Vector3 gravity;
   
    public Vector3 moveForce;
    Vector3 acceleration;
	Quaternion rotationDestination;
	public bool grounded;

    void UpdateMovement() {
        GetInput();
        feetPos = new Vector3(transform.position.x, transform.position.y - (charCollider.height / 2)+0.1f, transform.position.z);
        if (!grounded)
        {
            transform.up = Vector3.up;
            gravity = mass * (-Vector3.up * gravForce);
        } else {
          
            gravity = Vector3.zero;
            transform.up = groundPos.normal;
        }
        inputForce = (inputForce.x * (transform.right * inputX)) + (inputForce.z * (transform.forward * inputY));
        moveForce = inputForce + gravity;
          if (jumping && grounded) {
            moveForce += jumpForce;
        }
       
        // Add friction to rigidbody movement to stop unwanted movement
        Vector3 angularFriction = 100 * -playerRigidbody.angularVelocity;
        Vector3 rigidbodyFriction = 100 * -playerRigidbody.velocity;
        playerRigidbody.angularVelocity += angularFriction * Time.deltaTime;
        playerRigidbody.velocity += rigidbodyFriction * Time.deltaTime;
        // Calculate Movement
        friction = frictionCoefficent * -velocity;
        acceleration = (moveForce / mass) + friction;
        velocity += acceleration * Time.deltaTime;
       
        Vector3 newPosition = transform.localPosition + (velocity * Time.deltaTime);
        RaycastHit hit;
        if (playerRigidbody.SweepTest(velocity, out hit, velocity.magnitude * Time.deltaTime))
        {
       
            if (hit.collider.tag == "Death")
            {
                Kill();
            }
            if (hit.point.y < feetPos.y + stepOffset)
            {
                //grounded = true;
                groundPos = hit;
            }
           
            newPosition = transform.position + (velocity.normalized * hit.distance);
            
        }
     
    
        // Rotates towards velocity
		//if(inputY>-0.1f){
		//rotationDestination = Quaternion.LookRotation(new Vector3 (velocity.x,0,velocity.z));
		//transform.rotation = Quaternion.RotateTowards(transform.rotation,rotationDestination,turnSpeed*Time.deltaTime);
		//}
		transform.position = newPosition;

//		rigidbody.MovePosition (rigidbody.position + position);
	}
    void OnCollisionStay(Collision col)
    {

        float bump = 0;
        for (int i = 0; i < col.contacts.Length; i++)
        {
            if (col.contacts[i].point.y < feetPos.y + stepOffset && col.contacts[i].point.y > feetPos.y && grounded)
            {
                if (col.contacts[i].point.y >= bump)
                {
                    bump = col.contacts[i].point.y - feetPos.y;
                }

            }
            transform.position = new Vector3(transform.position.x, transform.position.y + bump, transform.position.z);
        }

    }

    public void AddVelocity(Vector3 force){
	
		velocity += force;
	}
	void GroundCheck(){
	
		RaycastHit check;
		Ray downRay = new Ray (transform.position, -transform.up);

		if (Physics.Raycast (downRay, out check)) {
			Debug.DrawLine (transform.position,check.point);
			if (check.distance <= (charCollider.height/2)+.5f)  {
				if (check.collider.tag == "Death"){
					infoScript.Kill();
				}
				if(check.collider.tag == "win"){
				//	infoScript.camScript.win = true;
				}
                grounded = true;
                groundPos = check;
				frictionCoefficent = initialFrictionCoefficent;
			}else{
				grounded = false;
				frictionCoefficent = airFrictionCoefficent;
			}
		} else{
			frictionCoefficent = airFrictionCoefficent;
			grounded = false;
		}

	}
	void Swing(){
	//if (script != null) {
			//script.Swing (acceleration);		
	//	}
	}
	public void Kill(){
		velocity = Vector3.zero;
		acceleration = Vector3.zero;

	}
	                  


}
//for cool things
//Vector3 normalForce;
//normalForce = Vector3.Dot(groundPos.normal, gravity) * groundPos.normal;