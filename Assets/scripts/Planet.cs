﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {


	public float gravity;
	public float speed;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position -= transform.up * Time.deltaTime * speed;
	}
}
