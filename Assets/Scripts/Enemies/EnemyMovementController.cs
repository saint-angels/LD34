﻿using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

	float speed = 5f;
	public Vector3 movementVector;
	public bool moving = true;
	int direction = 1;

	private int playerMask;

	private EnemyGraphics enemyGraphics;

	// Use this for initialization
	void Start () {
		playerMask = LayerMask.GetMask("Player");	
		enemyGraphics = GetComponent<EnemyGraphics> ();
	}

	public void SetDirection (int startDirection) {
		direction = startDirection;
		movementVector = Vector3.right * startDirection * speed;
	}

	void Update () {
		if (moving) {
			transform.Translate( movementVector * Time.deltaTime);	
			CheckForPlayerAhead();
		}
	}

	void StartMoving () {
		moving = true;
		enemyGraphics.StartMoving();
	}

	void StopMoving () {
		moving = false;
		enemyGraphics.StopMoving();
	}

	void CheckForPlayerAhead () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*direction, 1f, playerMask);
		if (hit.collider != null) {
			moving = false;
		}
	}



}
