using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "planet") {
			Destroy (col.gameObject);
		}

		if(col.tag == "Player"){
			// Game over
			Debug.Log("GAME OVER");
		}
	}
}
