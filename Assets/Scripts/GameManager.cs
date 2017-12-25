using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	public FoodManager foodManagerScript;

	public Const.KosherStatus CurrentAnswer;

	private float timer = 0.0f;
	private int numSecondsPast = 0;
	private int currentLevel = 1;

	private int GAME_SPEED = 1;
	private int START_NEW_SCENE_PAUSE = 2;

	void Awake () 
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		foodManagerScript = GetComponent<FoodManager> ();
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
		if (scene.name != "main") {
			return;
		}

		string levelArg = SceneArgs.getArg ("level");
		int level = 1;

		if (levelArg == null) {
			level = 1;
		} else {
			level = Convert.ToInt32(levelArg);
		}

		currentLevel = level;
		StartLevel();
	}

	private void StartLevel()
	{
		Debug.Log ("GameManager: Starting level " + currentLevel);
		//ResetPot ();
		foodManagerScript.SetLevel (currentLevel);
		foodManagerScript.Reset ();
	}

	public void StartNextLevel()
	{
		currentLevel += 1;
		StartLevel ();
	}

	public void RestartLevel()
	{
		StartCoroutine(RestartLevelAfterPause(START_NEW_SCENE_PAUSE));
	}

	IEnumerator RestartLevelAfterPause(int seconds)
	{
		Debug.Log ("GameManager: Restarting level " + currentLevel);
		yield return new WaitForSeconds (seconds);
		StartLevel();
	}
		
	public void StartNextLesson()
	{
		StartCoroutine(StartNextLessonAfterPause(START_NEW_SCENE_PAUSE));
	}

	IEnumerator StartNextLessonAfterPause(int seconds)
	{
		yield return new WaitForSeconds (seconds);
		SceneArgs.clear ();
		SceneArgs.addArg ("level", (currentLevel + 1).ToString());
		SceneManager.LoadScene("Lessons");
	}

	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime * GAME_SPEED;
		int seconds = (int)timer;
		if (seconds > numSecondsPast) {
			foodManagerScript.SpawnObject ();
			numSecondsPast += 1;
		}
	}

	public void addFoodToPot (GameObject food)
	{
		foodManagerScript.addFoodToPot (food);
	}
		
}
