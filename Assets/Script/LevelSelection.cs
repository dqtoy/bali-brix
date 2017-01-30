using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
	public static int highestLevel = 1;
	public Sprite[] levelStates;

	private GameObject[] levels;


	// Use this for initialization
	void Start ()
	{
		print ("highestLevel:" + highestLevel); 
		print (PlayerPrefs.GetInt ("Highest Level")); 

		levels = GameObject.FindGameObjectsWithTag ("Level Selectors");
		if (PlayerPrefs.HasKey ("Highest Level")) {
			highestLevel = PlayerPrefs.GetInt ("Highest Level");
		} else {
			PlayerPrefs.SetInt ("Highest Level", highestLevel);
		}
		SetLocks ();
	}

	private void SetLocks ()
	{
		print ("levels.Length:" + levels.Length);
		print ("highestLevel:" + highestLevel); 
		for (int i = 0; i < levels.Length; i++) {
			if (i < highestLevel) {
				levels [i].GetComponent <Image> ().sprite = levelStates [1];
			} else {
				levels [i].GetComponent <Image> ().sprite = levelStates [0];
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		// get the text property 
		// set them to numbers or lock ison based on the value of HighestLevel
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
