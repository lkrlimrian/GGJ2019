using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

	public GameObject scGameOverNS;
	public GameObject scGameOverS;
	public GameObject scInstruction;
	public GameObject recordHS;
	public GameObject newrecordS, scorex;

	// Use this for initialization
	void Start () {
		scGameOverNS.SetActive (false);
		scGameOverS.SetActive (false);
		recordHS.GetComponent<Text> ().text = PlayerPrefs.GetInt("score").ToString();

		/*if (PlayerPrefs.GetInt ("played") == 1) { 
			scInstruction.SetActive (false);
		} else { scInstruction.SetActive (true); }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void CloseInstructions(){
		PlayerPrefs.SetInt ("played", 1);
		scInstruction.SetActive (false);
	}
}
