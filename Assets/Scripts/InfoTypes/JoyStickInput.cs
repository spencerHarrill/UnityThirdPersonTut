using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class JoyStickInput
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
	public JoystickPos direction;
	public float accuracy;
	// Use this for initialization
	public JoyStickInput(int direction, float accuracy)
	{
		this.direction = (JoystickPos)direction;
		this.accuracy = accuracy;
		//Debug.Log (accuracy);
	}

	
}
