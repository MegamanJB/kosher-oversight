using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public static bool showKosherText = false;
	public static bool showIncorrectText = false;
	public static bool showCorrectText = false;

	private string kosherText = "Is the pot kosher? Drag the pot to the right place.";
	private string correctText = "Correct!";
	private string incorrectText = "That is not correct.";

	private Text textComponent;

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text>();
	}

	public static void resetText()
	{
		showKosherText = false;
		showIncorrectText = false;
		showCorrectText = false;
	}

	public static void doShowKosherText()
	{
		resetText ();
		showKosherText = true;
	}

	public static void doShowCorrectText()
	{
		resetText ();
		showCorrectText = true;
	}

	public static void doShowIncorrectText()
	{
		resetText ();
		showIncorrectText = true;
	}

	
	// Update is called once per frame
	void Update () {
		if (showKosherText) {
			textComponent.text = kosherText;
		} 
		else if (showCorrectText) {
			textComponent.text = correctText;
		}
		else if (showIncorrectText) {
			textComponent.text = incorrectText;
		}
		else {
			textComponent.text = "";
		}
	}
}
