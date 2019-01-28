using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// Player
	Rigidbody2D rb;
	GameObject boost;
	int life = 3;
	public GameObject scGameOverNS, scGameOverS, newhighscore, currscore, recordhs;

	//bool powerup;
	public float jump;
	public float steerspeed;
	int score, timer;
	Text scoreUI;
	SpriteRenderer sr;
	bool start;
	AudioSource whoosh;
	public AudioSource GO, crash;

	GameObject earth;

	Vector3 position;

	// Use this for initialization
	void Start () {
		//powerup = false;

		Screen.SetResolution (360, 640, false);
		boost = GameObject.Find("boost");
		boost.SetActive (false);
		sr = gameObject.GetComponent<SpriteRenderer> ();
		Time.timeScale = 0;
		start = false;
		earth = GameObject.Find ("earth");
		scoreUI = GameObject.Find ("score").GetComponent<Text>();
		scoreUI.text = "";

		whoosh = gameObject.GetComponent<AudioSource> ();

		//PlayerPrefs.SetInt ("score", 0); //clear high score
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
				whoosh.Play ();
				boost.SetActive (true);

			}
		}

		if (start == true) {
			if(earth != null){
				if (earth.transform.position.y > -15) {
					earth.transform.position -= transform.up * Time.deltaTime * 2.2f;
				} else {
					Destroy (earth);
				}
			}
		}

		if (!(gameObject.transform.position.y > -8 && gameObject.transform.position.y < 8)) {
			Time.timeScale = 0;
			//Debug.Log ("GAME OVER - LOST IN OBLIVION");

			GameOver ();

		}

		gameObject.transform.position += CrossPlatformInputManager.GetAxisRaw ("Horizontal") * transform.right * Time.deltaTime * steerspeed;
	}

	void FixedUpdate(){
		scoreUI.text = score.ToString();
		score++;
		if(score % 500 == 0){
			scoreUI.text = (int.Parse (scoreUI.text)/500).ToString();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "orbit") {
			jump += col.gameObject.GetComponent<Planet>().gravity;
			steerspeed += col.gameObject.GetComponent<Planet>().gravity;

			//Debug.Log ("G+");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "orbit") {
			jump -= col.gameObject.GetComponent<Planet>().gravity;
			steerspeed -= col.gameObject.GetComponent<Planet>().gravity;
			//Debug.Log ("G-");
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "planet") {
			if (life > 0) {
				GameObject.Destroy (GameObject.Find ("heart" + life.ToString ()));
				life -= 1;
				crash.Play ();
				//Debug.Log ("lives left: " + life);
			} else {
				//Debug.Log ("GAME OVER BY LIFE");
				boost.SetActive (false);
				//sr.sprite = Resources.Load<Sprite> ("onecGO") as Sprite;
				Time.timeScale = 0;

				GameOver ();
			}
		}
	}

	void GameOver(){
		if (int.Parse (scoreUI.text) < PlayerPrefs.GetInt ("score")) {
			currscore.GetComponent<Text>().text = scoreUI.text;
			recordhs.GetComponent<Text>().text = PlayerPrefs.GetInt ("score").ToString();
			scGameOverS.SetActive (true);	
		} else {
			PlayerPrefs.SetInt ("score", int.Parse (scoreUI.text)); //NEW HIGH SCORE
			//currscore.GetComponent<Text>().text = scoreUI.text;
			newhighscore.GetComponent<Text>().text = scoreUI.text;
			scGameOverNS.SetActive (true);
		}
	}
}
