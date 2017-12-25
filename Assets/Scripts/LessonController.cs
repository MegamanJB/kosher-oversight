using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LessonController : MonoBehaviour {

	private Text lessonText;

	private string[] lessons = {
		"You may not benefit in any way from milk and meat cooked together. It must be thrown out.",
		"Chicken and milk is only forbidden rabinnically and may be benefited from, such as by giving to dogs.",
		"Meat and milk that was not cooked together, but only soaked for 24 hours, is only forbidden rabinnically and may be benefited from, such as by giving to dogs.",
		"If 1/60th or less of milk was accidentally added to a cooking meat, the food is permitted completely.",
	};

	void Awake () {
		lessonText  = GameObject.Find ("lessonText").GetComponent<Text>();
	}

	void OnEnable()
	{
		//Tell our 'OnSceneLoaded' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		//Tell our 'OnSceneLoaded' function to stop listening for a scene change as soon as this script is disabled.
		//Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		string levelArg = SceneArgs.getArg ("level");
		int level = 1;

		if (levelArg == null) {
			level = 1;
		} else {
			level = Convert.ToInt32(levelArg);
		}

		loadLesson (level);
	}

	void Update () {

		if (Input.anyKey) {
			SceneManager.LoadScene("Main");
		}
		
	}

	public void loadLesson(int lessonNum)
	{
		Debug.Log ("LessonController: Loading level number " + lessonNum);

		string nextLessonText = "Lesson " + lessonNum + ":\n";
		nextLessonText += lessons [lessonNum - 1];
		lessonText.text = nextLessonText;
	}
}
