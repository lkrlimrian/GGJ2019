using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {

	public GameObject[] planets;
	int planetNo;
	public float maxpos = 6f;
	public float delaytimer = 0f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = delaytimer;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Vector3 planetPos = new Vector3 (Random.Range (-2, 2), 8, transform.position.z);
			planetNo = Random.Range (0, planets.Length);
			Instantiate (planets [planetNo], planetPos, transform.rotation);
			timer = delaytimer;
		}
	}
}
