using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEditor;

public class LevelSelection : MonoBehaviour
{
	public static int highestLevel = 1;
	public static int currentPage = 0;
	public Sprite[] levelStates;
	public Sprite[] levelStars;

	private GameObject[] levels;
	private int stars = 0;

	// Use this for initialization
	void Start ()
	{
		ReadPlayersPref ();
		AssembleCurrentPageButtons (currentPage);
		// sort them right at the initialization or Unity goes mad
		levels = GameObject.FindGameObjectsWithTag ("Level Selectors").OrderBy (c => c.name).ToArray ();
		HandleLocks ();
	}

	private void ReadPlayersPref ()
	{
		// set the highest level
		if (PlayerPrefs.HasKey ("Highest Level")) {
			highestLevel = PlayerPrefs.GetInt ("Highest Level");
		} else {
			PlayerPrefs.SetInt ("Highest Level", highestLevel);
		}

		// set the current page
		if (PlayerPrefs.HasKey ("Current Page")) {
			currentPage = PlayerPrefs.GetInt ("Current Page");
		} else {
			PlayerPrefs.SetInt ("Current Page", currentPage);
		}
	}


	private void AssembleCurrentPageButtons (int page)
	{
		// ToDo:
		int index = currentPage * 10 + 1;
		for (i = index; i < index + 10; i++) {
			//	GameObject newButton = 
			//		Instantiate(buttonPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
			//	newButton.transform.SetParent(loadCanvas.transform, true); 
		}
		// create a child game object for canvas + set is location
		// attach button component to it
		// attach script for loading level tto it
		// create child game object for level number + set is location
		// create child game object for level stars + set is location
	}

	// We need two sets of buttons: One inactive with the lock icon
	// the other one active with green button,  level number and number of stars
	private void HandleLocks ()
	{
		for (int i = 0; i < levels.Length; i++) {
			if (i < highestLevel) {
				levels [i].GetComponent <Image> ().sprite = levelStates [1]; // green icon
				levels [i].GetComponent <Button> ().interactable = true;
				foreach (Transform t in levels[i].transform) {
					if (t.name == "number") {
						t.GetComponent <Text> ().text = (i + 1).ToString (); //48.ToString ();//
					} else if (t.name == "stars") { 
						stars = PlayerPrefs.GetInt ("Level" + (i + 1).ToString ());
						t.GetComponent <Image> ().sprite = levelStars [stars];
					}
				}
			} else {
				levels [i].GetComponent <Image> ().sprite = levelStates [0]; // lock icon
				levels [i].GetComponent <Button> ().interactable = false;
				foreach (Transform t in levels[i].transform) {
					if (t.name == "stars") { 
						t.GetComponent <Image> ().sprite = levelStars [0];
					}
				}
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
		// Need to reset the brick counts otherwise the leftover from last game 
		// will be added to the new game
		Brick.brickCounts = 0;
		Ball.hasStarted = false;
		SceneManager.LoadScene (name);
	}
}
