﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class FallingObjects : MonoBehaviour
{
	public Sprite[] fallingObjects;
	public AudioClip powerUp, powerDown;
	public GameObject 
		coin, gun, sealer, speedUp, slowDown, 
		growPaddle, shrinkPaddle, breakThrough,
		tripleBall, growBall, shrinkBall;

	private float blinkDuration = 0.3f;
	private LevelManager levelManager;
	private GameObject ballCount;
	private GameObject paddle;

	// Use this for initialization
	void Start ()
	{
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		paddle = GameObject.Find ("Paddle");
		ballCount = GameObject.Find ("Balls No");
		ballCount.GetComponent <Text> ().text = LevelManager.ballCounts.ToString ();
	}

	// Update is called once per frame
	void Update ()
	{
		this.GetComponent <SpriteRenderer> ().color = 
			new Color (255f, 255f, 255f, Mathf.PingPong (Time.time, blinkDuration) / blinkDuration + 0.5f);	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// What should we do next when we catch a falling object
		if (other.tag == "Paddle") {
			Destroy (this.gameObject);
			switch (this.GetComponent <SpriteRenderer> ().sprite.name) {
			case "balls_1":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				int currentBonus = Random.Range (4, 26);
				LevelManager.coins += currentBonus;
				GameObject coinClone = 
					Instantiate (coin, new Vector3 (0f, 0f, 0f), transform.rotation) as GameObject;
				levelManager.ShowMessage ("+" + currentBonus.ToString () + " coins");
				break;
			case "balls_2":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				GameObject gunClone = 
					Instantiate (gun, new Vector3 (0f, 0f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_3":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				GameObject sealerClone = 
					Instantiate (sealer, new Vector3 (0f, -5.1f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_4":
				AudioSource.PlayClipAtPoint (powerDown, this.transform.position);	
				GameObject speedUpClone = 
					Instantiate (speedUp, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_5":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				GameObject slowDownClone = 
					Instantiate (slowDown, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_6":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				GameObject growPaddleClone = 
					Instantiate (growPaddle, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_7":
				AudioSource.PlayClipAtPoint (powerDown, this.transform.position);	
				GameObject shrinkPaddleClone = 
					Instantiate (shrinkPaddle, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_8":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				AddLife ();
				break;
			case "balls_9":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				levelManager.ShowMessage ("DURIAN");
				GameObject breakthroughClone = 
					Instantiate (breakThrough, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_10":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				GameObject tripleBallClone = 
					Instantiate (tripleBall, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_11":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				GameObject growBallClone = 
					Instantiate (growBall, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			case "balls_12":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				GameObject shrinkBallClone = 
					Instantiate (shrinkBall, new Vector3 (0f, -5.8f, 0f), transform.rotation) as GameObject;
				break;
			// removed for now
			/*case "balls_11":
				AudioSource.PlayClipAtPoint (powerUp, this.transform.position);	
				PauseBall ();
				break;*/
			default:
				print ("you hit the R ball!");
				break;
			}
		}
	}

	public Sprite SetTheBall (int index)
	{
		return fallingObjects [index];
	}

	private void AddLife ()
	{
		levelManager.AddBonusBall (ballCount);
		// we need to decrease the bonus factor because in the AddBonusBall it 
		// has been increased for handling score bonus
		Ball.bonusFactor--;
	}

	private void PauseBall ()
	{
		// Removed for now
		// ToDo: active the trigger for paddle
		// Find the number of active balls
		// when a BALL hit the triggered paddle decrease the numberof active balls
		// and freeze the ball where ever it is
		// (possibly deactivate the gun as well)
		// when all balls are caught (or missed)
		// each tap should release one ball
		// by releasing the last ball set the paddle's isTrigger to false
	}
}