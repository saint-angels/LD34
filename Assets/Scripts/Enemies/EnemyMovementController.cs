﻿using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

	float speed = 5f;
	public Vector3 movementVector;
	public bool moving = true;
	int direction = 1;




	// Use this for initialization
	void Start () {
	}

	public void SetDirection (int startDirection) {
		direction = startDirection;
		movementVector = Vector3.right * startDirection * speed;
	}

	void Update () {
		if (moving) {
			transform.Translate( movementVector * Time.deltaTime);	
		}
	}

	public void StartMoving () {
		moving = true;
	}

	public void StopMoving () {
		moving = false;
	}
}
