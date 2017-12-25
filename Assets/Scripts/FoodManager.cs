using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class FoodManager : MonoBehaviour {

	public GameObject[] foods;
	public GameObject spawnPoint;
	public GameObject foodListBeginPoint;
	public GameObject foodInPotPrefab;
	public GameObject pot;

	List<FoodMixture> potSequences = new List<FoodMixture> ();

	private Dictionary<string, GameObject> foodsByName = new Dictionary<string, GameObject>();
	private List<GameObject> foodInPotList = new List<GameObject>();
	Vector3 foodListOffset;

	private int currentSequence = 0;
	private int currentIndexInSequence = 0;
	private bool doSpawn = true;

	public void Awake()
	{
		foreach (var food in foods) {
			food.GetComponent<FoodScript>().foodName = food.name;
			foodsByName.Add (food.name, food);
		}

		potSequences.Add(new FoodMixture(new string[] {"steak", "cheese", "fire"}, Const.KosherStatus.Cannot_benefit));
		potSequences.Add(new FoodMixture(new string[] {"drumstick", "cheese", "fire"}, Const.KosherStatus.Nonkosher));

		//potSequences.Add(new List<string>() { "banana", "onion"});
		//potSequences.Add(new List<string>() { "steak", "water", "cheese", "-steak" });
		//potSequences.Add(new List<string>() { "cheese", "water", "heat", "-cheese" , "hourglass", "burger"});
		//potSequences.Add(new List<int>() { "kosher meat", "kosher meat", "non-kosher meat" });
		//potSequences.Add(new List<int>() { 3, 2, 1, -1, 3, -2 });

		SpriteRenderer renderer = foods[0].GetComponent<SpriteRenderer>();
		foodListOffset = new Vector3(0, renderer.bounds.size.y, 0);
	}

	private GameObject getPot()
	{
		if (pot == null) {
			pot = GameObject.Find ("pot");
		}
		return pot;
	}

	public void SetLevel(int level)
	{
		currentSequence = level - 1;
		if (currentSequence >= potSequences.Count) {
			currentSequence = level % potSequences.Count;
		}
	}

	public void Reset()
	{
		foreach (var food in foodInPotList) {
			Destroy (food);
		}
		TextManager.resetText();
		resetPotPosition ();
		foodInPotList.Clear ();
		currentIndexInSequence = 0;
		doSpawn = true;
	}

	public void SpawnObject()
	{
		if (!doSpawn) {
			return;
		}

		FoodMixture potSequence = potSequences [currentSequence];
		GameManager.instance.CurrentAnswer = potSequence.kosherStatus;

		if (currentIndexInSequence >= potSequence.Foods.Count) {
			TextManager.doShowKosherText();
			doSpawn = false;
			return;
		}

		string nextFood = potSequence.Foods [currentIndexInSequence];

		if (!nextFood.StartsWith("-")) {
			//Debug.Log (nextFood);
			GameObject food = foodsByName [nextFood];
			Instantiate (food, spawnPoint.transform.position, Quaternion.identity);
		
		} else {
			popFoodFromPot (nextFood.Remove(0, 1));
		}

		currentIndexInSequence += 1;
	}

	public void addFoodToPot(GameObject food)
	{
		Sprite foodSprite = food.GetComponent<SpriteRenderer>().sprite;
		Vector3 offset = foodInPotList.Count * foodListOffset * -1;

		GameObject newFood = Instantiate (foodInPotPrefab, foodListBeginPoint.transform.position + offset, Quaternion.identity);

		newFood.transform.localScale = food.transform.localScale;

		SpriteRenderer renderer = newFood.GetComponent<SpriteRenderer>();
		renderer.sprite = foodSprite;

		newFood.GetComponent<foodInPot>().foodName = food.GetComponent<FoodScript>().foodName;

		foodInPotList.Add (newFood);
	}

	private void relayoutFoodInPotList()
	{
		for (int i = 0; i < foodInPotList.Count; i++)
		{
			GameObject food = foodInPotList[i];
			Vector3 offset = (i) * foodListOffset * -1;
			food.transform.position = foodListBeginPoint.transform.position + offset;
		}
	}

	public void popFoodFromPot(string foodToRemove)
	{
		GameObject poppingFood = null;
		foreach (var food in foodInPotList) {
			//Debug.Log ("in pot " + food.GetComponent<foodInPot>().foodName);
			if (food.GetComponent<foodInPot>().foodName == foodToRemove) {
				poppingFood = food;
			}
		}

		if (!poppingFood) {
			//Debug.Log("Food " + foodToRemove + " not found in pot");
			return;
		}

		foodInPotList.Remove(poppingFood);

		poppingFood.transform.position = getPot().transform.position;
		Rigidbody2D rb2d = poppingFood.GetComponent<Rigidbody2D>();
		Vector2 poppingForce = new Vector2(-100, 150);
		rb2d.AddForce(poppingForce);
		rb2d.gravityScale = 1;

		relayoutFoodInPotList();
	}

	public void resetPotPosition()
	{
		getPot().GetComponent<PotController> ().resetPotPosition ();
	}
		
}
