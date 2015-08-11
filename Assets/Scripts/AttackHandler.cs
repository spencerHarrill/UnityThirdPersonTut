using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackHandler : MonoBehaviour
{
	public List<JoyStickInput> inputs;
	public List<Combo> completeCombos;
	public List<Combo> combos;
	public string currentInput;
	// Use this for initialization
	// for sorting possible combos
	private static int CompareCombosByLength (Combo x, Combo y)
	{
		if (x.comboInput == null) {
			if (y.comboInput == null) {
				return 0;
			} else {
				return -1;
			}
		} else {
			if (y.comboInput == null) {
				return 1;
			} else {
				int retval = x.comboInput.Length.CompareTo (y.comboInput.Length);
				if (retval != 0) {
					return -retval;
				} else {
					return x.comboInput.CompareTo (y.comboInput);
				}
			}
		}
	}

	void Start ()
	{
		inputs = new List<JoyStickInput> ();
		//sorts the combos to longest first for search purposes
		combos.Sort (CompareCombosByLength);
	}
// Update is called once per frame
	void Update ()
	{
		if (currentInput.Contains ("0")) {
			ComboCheck ();
			inputs.Clear ();
		}
		
	}

	public void CreateInput (int direction, float accuracy)
	{
		inputs.Add (new JoyStickInput (direction, accuracy));
		currentInput += direction.ToString ();

	}

	void ComboCheck ()
// checks for valid input combos in current input
	{
		for (int i = 0; i < combos.Count; i++) {
			string currentComboCheck = combos [i].comboInput;
			if (currentInput.Contains (currentComboCheck)) {
				int index = currentInput.IndexOf (currentComboCheck);
				CreateCombo (combos [i]);
			}
		}
	}

	void CreateCombo (Combo combo)
// creates a reference to the current combo
	{

		completeCombos.Add(combo);
		Debug.Log (combo);
		inputs.Clear ();
		currentInput = "";

	}
}
