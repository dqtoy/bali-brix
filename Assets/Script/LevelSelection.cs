using UnityEngine;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
	public static int highestLevel;
	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt ("HighestLevel", 1);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
