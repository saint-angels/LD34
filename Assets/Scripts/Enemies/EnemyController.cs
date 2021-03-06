﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {


	public enum EnemyState
	{
		MOVING_TO_PLAYER,
		WAITING_TO_PUNCH,
		PUNCHING,
		PUNCHING_ANIMATION,
		DEAD
	}


	public EnemyState state = EnemyState.MOVING_TO_PLAYER;
	EnemyMovementController movement;
	EnemyGraphics enemyGraphics;
	EnemyPunchingController enemyPunch;

	int playerMask;

	public float punchWait = 1.5f;
	public bool movingRight;
	public float punchWaitLeft = 1.5f;

	public float punchAnimationDelay = 0.2f;



	// Use this for initialization
	virtual protected void Start () {
	}

	void Awake() {
		movement = GetComponent<EnemyMovementController> ();
		enemyGraphics = GetComponent<EnemyGraphics> ();
		enemyPunch = GetComponent<EnemyPunchingController> ();

		playerMask = LayerMask.GetMask("Player");	
	}
	
	// Update is called once per frame
	virtual protected void Update () {
		switch (state) {
		case EnemyState.MOVING_TO_PLAYER:
			if (PlayerAhead()) {
				state = EnemyState.WAITING_TO_PUNCH;
				StopMovingToPlayer();
				punchWaitLeft = punchWait;
			}else {
				StartMovingToPlayer();
			}
			break;
		case EnemyState.WAITING_TO_PUNCH:
			if (punchWaitLeft < 0) {
				state = EnemyState.PUNCHING;
			}else {
				if (PlayerAhead()) {
					punchWaitLeft -= Time.deltaTime;
				}else {
					state = EnemyState.MOVING_TO_PLAYER;
				}
			}
			break;
		case EnemyState.PUNCHING:
			enemyGraphics.Punch();
			Invoke("StartPunching", punchAnimationDelay);
			state = EnemyState.PUNCHING_ANIMATION;
			break;
		default:
			break;
		}
	}

	void StartPunching() {
		if (PlayerAhead()) {
			enemyPunch.PunchPlayer();
			state = EnemyState.WAITING_TO_PUNCH;
			punchWaitLeft = punchWait;	
		}else {
			state = EnemyState.MOVING_TO_PLAYER;
		}
	}

	public void StartMovingToPlayer () {
		movement.SetDirection(dirFromRight(movingRight));				
		movement.StartMoving();
		enemyGraphics.FlipX(!movingRight);
		enemyGraphics.StartMoving();
		enemyPunch.movingRight = movingRight;
	}

	public void StopMovingToPlayer () {
		movement.StopMoving();
		enemyGraphics.StopMoving();
	}

	public void Die () {
		StopMovingToPlayer();
		enemyGraphics.Die();
		gameObject.layer = 10;//LayerMask.GetMask("DeadEnemy");
		state = EnemyState.DEAD;
		GetComponent<EnemyController>().enabled = false;
	}

	bool PlayerAhead () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*dirFromRight(movingRight), 1f, playerMask);
		if (hit.collider != null) {
			return true;
		}
		return false;
	}

	int dirFromRight(bool movingRight) {
		return movingRight ? 1 : -1;
	}
}
