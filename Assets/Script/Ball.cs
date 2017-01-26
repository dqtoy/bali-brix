﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public AudioClip hit;
	public static bool hasStarted = false;
	public static bool bonusPaid = false;
	public static int bonusFactor = 1;

	private Paddle paddle;
	private Vector3 paddleToBallVector;

	// Use this for initialization
	void Start ()
	{
		paddle = GameObject.FindObjectOfType<Paddle> ();
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!hasStarted) {
			this.transform.position = paddle.transform.position + paddleToBallVector;
			if (Input.GetButtonDown ("Fire1")) {
				this.GetComponent <Rigidbody2D> ().velocity = new Vector2 (2.25f, 9.1f);
				hasStarted = true;
			}		
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Vector2 speedUp = new Vector2 (Random.Range (-0.35f, 0.35f), Random.Range (-0.1f, 0.35f));
		if (hasStarted) {
			AudioSource.PlayClipAtPoint (hit, this.transform.position);	
			this.GetComponent <Rigidbody2D> ().velocity += speedUp;
			if (Brick.brickCounts <= 0) // Make sure everything stops after last brick was hit
				Destroy (this.gameObject);
		}		
	}
}