﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using System;

public class Brick : MonoBehaviour
{
	// we use statis to make sure there is only one variable across all Brick classes
	// that way we can increase/decrease the same variable inside each Brick independently
	public static int brickCounts = 0;
	public AudioClip crack;
	public Sprite[] hitSprites;
	public GameObject particles;
	public GameObject fallingObject;

	private bool hasBall = false;
	//private int fallingBallIndex = 1;
	private int timesHit, ballIndex;
	private LevelManager levelManager;
	private GameObject score, levelCompleteScore;
	private GameObject balls, dust;

	// Use this for initialization
	void Start ()
	{
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		score = GameObject.Find ("Score");
		score.GetComponent <Text> ().text = LevelManager.currentScore.ToString ();
		levelCompleteScore = GameObject.Find ("Level Complete Score");
		if (levelManager.fallingObjects > 0 && Random.Range (0f, 1f) > 0.5f) {
			hasBall = true;
			levelManager.fallingObjects--;
			SetBall ();
		}

		balls = GameObject.Find ("Balls No");
		balls.GetComponent <Text> ().text = LevelManager.ballCounts.ToString ();

		if (this.tag == "Breakable")
			brickCounts++;
	}

	private void SetBall ()
	{
		int maxBallIndex = (int)Mathf.Clamp (SceneManager.GetActiveScene ().buildIndex, 3, 8);
		//print ("ball: " + fallingBallIndex + " max: " + maxBallIndex); 
		//print ("calculations: " + fallingBallIndex % maxBallIndex);
		if (levelManager.fallingBallIndex == maxBallIndex)
			levelManager.fallingBallIndex = 1; //Random.Range (1, max);  
		else
			levelManager.fallingBallIndex++;
		ballIndex = levelManager.fallingBallIndex;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.transform.tag == "Ball" || collision.transform.tag == "Bullet") {
			//print ("COLLIDED and isTrigger is:" + this.GetComponent <BoxCollider2D> ().isTrigger.ToString ()); 
			AudioSource.PlayClipAtPoint (crack, this.transform.position);
			if (this.tag == "Breakable") {
				HandleScores ();
				HandleHits ();		
			}
		}
	}

	// use this for break through balls
	void OnTriggerEnter2D (Collider2D other)
	{
		//if (other.tag == "Ball") {
		if (this.tag == "Breakable" && other.tag == "Ball") {
			AudioSource.PlayClipAtPoint (crack, this.transform.position);
			//print ("TRIGGERED and isTrigger is:" + this.GetComponent <BoxCollider2D> ().isTrigger.ToString ());
			HandleScores ();
			HandleHits ();	
		}
	}

	void HandleScores ()
	{
		LevelManager.currentScore += 15;
		score.GetComponent <Text> ().text = LevelManager.currentScore.ToString ();
		levelCompleteScore.GetComponent <Text> ().text = LevelManager.currentScore.ToString ();
		if ((LevelManager.currentScore / (1000 * Ball.bonusFactor)) >= 1) {
			levelManager.AddBonusBall (balls);
		}
	}

	void HandleHits ()
	{
		timesHit++;
		if (timesHit >= hitSprites.Length + 1) {
			brickCounts--;
			UpdateView (this.gameObject);
			if (brickCounts <= 0) {
				levelManager.EvalDamage (0, true);
			}
		} else {
			LoadCrackedBrick ();
		}
	}

	void ShowParticles ()
	{
		dust = 
			Instantiate (particles, this.gameObject.transform.position, Quaternion.identity) as GameObject;
		dust.GetComponent<ParticleSystem> ().startColor = this.GetComponent<SpriteRenderer> ().color;
		Destroy (dust, 1f);
	}

	void UpdateView (GameObject brick)
	{
		ShowParticles ();
		Destroy (this.gameObject);
		if (hasBall) {
			DropBall (ballIndex);
		}
		levelManager.IncreaseBackgroundAlpha ();
	}

	void DropBall (int index)
	{
		GameObject ball = 
			Instantiate (fallingObject, this.gameObject.transform.position, Quaternion.identity) as GameObject;
		ball.GetComponent <SpriteRenderer> ().sprite = ball.GetComponent <FallingObjects> ().SetTheBall (index);
	}

	void LoadCrackedBrick ()
	{
		int index = timesHit - 1;
		if (hitSprites [index] != null)
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [index];
		else
			Debug.LogError ("Sprite is missing!");
	}
}
