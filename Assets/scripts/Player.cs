using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	// Player
	Rigidbody2D rb;
	GameObject boost;
	int life = 3;

	//bool powerup;
	public float jump;
	public float steerspeed;
	SpriteRenderer sr;
	bool start;

	Vector3 position;

	// Use this for initialization
	void Start () {
		//powerup = false;
		boost = GameObject.Find("boost");
		boost.SetActive (false);
		sr = gameObject.GetComponent<SpriteRenderer> ();
		Time.timeScale = 0;
		start = false;
	}
	
	// Update is called once per frame
	void Update () {
		//position.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

		//if (Input.touchCount > 0) {
		//}
		boost.SetActive (false);

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if (start == false) {
				start = true;
				Time.timeScale = 1;
			}
			if(Time.timeScale == 1){
				gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jump);
				boost.SetActive (true);

			}
		}

		if (start == true) {
			if (GameObject.Find ("earth").transform.up > -15) {
				GameObject.Find ("earth").transform.position -= transform.up * Time.deltaTime * 2.2f;
			} else {
				Destroy (GameObject.Find ("earth"));
			}
		}

		gameObject.transform.position += CrossPlatformInputManager.GetAxisRaw ("Horizontal") * transform.right * Time.deltaTime * steerspeed;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "orbit") {
			jump += col.gameObject.GetComponent<Planet>().gravity;
			Debug.Log ("G+");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "orbit") {
			jump -= col.gameObject.GetComponent<Planet>().gravity;
			Debug.Log ("G-");
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "planet") {
			if (life > 0) {
				life -= 1;
				Debug.Log ("lives left: " + life);
			} else {
				Debug.Log ("GAME OVER BY LIFE");
				boost.SetActive (false);
				//sr.sprite = Resources.Load<Sprite> ("onecGO") as Sprite;
				Time.timeScale = 0;
			}
		}
	}
}
