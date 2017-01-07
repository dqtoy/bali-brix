﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public void LoadLevel (string name)
	{
		Debug.Log ("load level requested:" + name);
		SceneManager.LoadScene (name);
	}

	public void LoadNextLevel ()
	{
		//int currentScene = SceneManager.sceneLoaded;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void Quit ()
	{
		Debug.Log ("quit!");
		Application.Quit ();
	}
}
