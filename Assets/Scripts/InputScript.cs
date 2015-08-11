using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputScript: MonoBehaviour
{
	public enum JoystickPos
	{
		Null,
		North, 
		NorthEast, 
		East, 
		SouthEast, 
		South, 
		SouthWest,
		West,
		NorthWest
		
	}
	;
	public JoystickPos currentPos;
	public JoystickPos previousPos;
	public AttackHandler attackHandler;
	public float inputAngle;
	List <float> circleAngles;
	float circleDivisions = 8f;
	// Use this for initialization
	void Start ()
	{
		attackHandler = gameObject.GetComponent<AttackHandler> ();

		circleAngles = new List<float> (); 
		//creates 8 circle sectors to reference input against
		for (int i = 0; i < circleDivisions; i++) {
			float angle = (i * (360 / 8) + 22.5f);
			if (angle < 0) {
				angle += 360;
			}
			circleAngles.Add (angle);
		}
	}

	void Update ()
	{
		float xInput = Input.GetAxis ("RHorizontal");
		float yInput = Input.GetAxis ("RVertical");
		//360 degree angle of input for input;
		if (xInput != 0 || yInput != 0) {
			float inputRadians = Mathf.Atan2 (xInput, yInput);
			inputAngle = Mathf.Rad2Deg * (inputRadians);
		} else {
			currentPos = (JoystickPos)0;  			//sets enum to null when no input; 
			Invoke ("CheckInputChange", 0.05f); 	// Checks null after a short delay so combos can pass through it without creating a null joystickInput.
			return;  								// return makes no code execute after it (goes back to Void Update).
		}
		if (inputAngle < 0) {
			inputAngle += 360; //dont want negative angles, and the angles we are finding only go from -180 to 180, so when we get negatives we add 360 to get 0 - 360 instead 
		}
		if (inputAngle >= 0) {
			CheckCircleSector ();
		}
		CheckInputChange ();
		previousPos = currentPos;
	}

	float GetAccuracy (float currentAngle, int currentPos)
	{
		if (currentPos > 0) {
			float targetAngle = circleAngles [currentPos - 1] - 22.5f; //Finds the Direct middle of that circle section as they are all 45 degrees apart, the minus one is because the enum has null at 0 and we want to ignore that
			return Mathf.Abs (targetAngle - currentAngle);
		} else {
			return -1f;
		}
	}

	void CheckInputChange () // checks if the current direction being inputted is different than the previous frame.
	{
		if (currentPos != previousPos) {
			previousPos = currentPos;
			attackHandler.CreateInput ((int)currentPos, GetAccuracy (inputAngle, (int)currentPos));//Tells the AttackHandler to create a joystickInputClass that relates to the current position of the new direction

		}
			
	}

	void CheckCircleSector ()
	{
		// references circle angles above to input to see what sector the input is in and assigns the enum correctly
		for (int i = 0; i < circleAngles.Count; i++) {
			if (inputAngle <= circleAngles [i]) {
				currentPos = (JoystickPos)i + 1;
				return;
			}
			
		}

	}
}


