using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

public class LevelSelection : MonoBehaviour
{
	public static int highestLevel = 1;
	public Sprite[] levelStates;

	private GameObject[] levels;


	// Use this for initialization
	void Start ()
	{
		//PlayerPrefs.SetInt ("Highest Level", 10);
		int index = 0;
		// sort them right at the initialization or Unity goes mad
		levels = GameObject.FindGameObjectsWithTag ("Level Selectors").OrderBy (c => c.name).ToArray ();
		if (PlayerPrefs.HasKey ("Highest Level")) {
			highestLevel = PlayerPrefs.GetInt ("Highest Level");
		} else {
			PlayerPrefs.SetInt ("Highest Level", highestLevel);
		}
		HandleLocks ();
	}

	private void HandleLocks ()
	{
		for (int i = 0; i < levels.Length; i++) {
			if (i < highestLevel) {
				print ("star: " + i); 
				print ("name: " + levels [i].name); 
				levels [i].GetComponent <Image> ().sprite = levelStates [1]; // star
				levels [i].GetComponent <Button> ().interactable = true;
			} else {
				print ("lock:" + i); 
				print ("name: " + levels [i].name); 

				levels [i].GetComponent <Image> ().sprite = levelStates [0]; // lock
				levels [i].GetComponent <Button> ().interactable = false;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		// get the text property 
		// set them to numbers or lock ison based on the value of HighestLevel
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public void LoadLevel (string name)
	{
		//levelManager.LoadLevel (name);

		Debug.Log ("load level requested:" + name);
		// Need to reset the brick counts otherwise the leftover from last game 
		// will be added to the new game
		Brick.brickCounts = 0;
		Ball.hasStarted = false;

		//Ball.ballCounts = 3;
		SceneManager.LoadScene (name);
	}

}
