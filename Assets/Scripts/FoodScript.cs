﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour {

	private Rigidbody2D rb2d;

	private float speed = 1f;

	public string foodName;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = new Vector2(0, -1 * speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
