using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour {

	private bool isMouseDown = false;
	private Vector3 mouseDownOffset;
	private bool enteredChoiceArea = false; 
	private Vector3 potStartingPosition;
	private string selectedObject = "";

	// Use this for initialization
	void Start () {
		potStartingPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		//if (Input.GetMouseButtonDown(0)) {

		if (isMouseDown) {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
			//mousePosition.Set (mousePosition.x, mousePosition.y, 0);
			gameObject.transform.position = mousePosition - mouseDownOffset;
		}

		//} else if (resetPotPosition) {
		//	gameObject.transform.position = potStartingPosition;
		//	resetPotPosition = false;
		//}
	}

	public void resetPotPosition()
	{
		if (potStartingPosition.magnitude == 0) {
			potStartingPosition = gameObject.transform.position;
		}
		gameObject.transform.position = potStartingPosition;
	}
		
	void OnMouseDown()
	{
		isMouseDown = true;
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		mouseDownOffset = mousePosition - gameObject.transform.position;
	}

	void OnMouseUp()
	{
		isMouseDown = false;

		if (enteredChoiceArea) {

			bool isCorrect = false;
			Const.KosherStatus correctAnswer = GameManager.instance.CurrentAnswer;

			Const.KosherStatus selectedKosherStatus = Const.SelectedAnswerKosherStatus [selectedObject];

			if (selectedKosherStatus == correctAnswer) {
				TextManager.doShowCorrectText();
				GameManager.instance.StartNextLesson ();

			} else {
				TextManager.doShowIncorrectText();
				GameManager.instance.RestartLevel ();

			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log (other.tag);
		List<string> choiceTags = new List<string> () { "jew", "dog", "trashcan" };


		if (other.CompareTag ("food")) {
			GameManager.instance.addFoodToPot (other.gameObject);
			Destroy (other.gameObject);

		} else if (choiceTags.Contains (other.tag)) {
			selectedObject = other.tag;
			enteredChoiceArea = true;

		}
	}
		
}
