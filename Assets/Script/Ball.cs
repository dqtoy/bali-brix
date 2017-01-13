﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public AudioClip hit;
	public static int ballCounts = 3;

	private Paddle paddle;
	private Vector3 paddleToBallVector;
	private bool hasStarted = false;
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
			if (Input.GetMouseButtonDown (0)) {
				this.GetComponent <Rigidbody2D> ().velocity = new Vector2 (2.2f, 9f);
				hasStarted = true;
			}		
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Vector2 speedUp = new Vector2 (Random.Range (-0.1f, 0.2f), Random.Range (-0.1f, 0.2f));
		if (hasStarted) {
			AudioSource.PlayClipAtPoint (hit, this.transform.position);	
			this.GetComponent <Rigidbody2D> ().velocity += speedUp;
		}		
	}
}