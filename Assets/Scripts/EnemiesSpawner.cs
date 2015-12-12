﻿using UnityEngine;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour {


	public Transform leftSpawnPoint;
	public Transform rightSpawnPoint;
	public GameObject enemyPrefab;
	public Transform playersTransform;

	float spawnTimeOut = 1f;
	bool lastSpawnRight = false;
	private bool DEBUG_SPEED = false;

	// Use this for initialization
	void Start () {
		spawnTimeOut = Random.Range(0f, 1f);
		StartCoroutine("SpawnEnemies");
	}

	void Update () {
		

		if (Input.GetButtonDown("Jump")) {
			DEBUG_SPEED = !DEBUG_SPEED;
		}
	}





	IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(spawnTimeOut);
			spawnTimeOut = DEBUG_SPEED ? 0.1f : Random.Range(.75f, 2f);

			GameObject newEnemy;
			int runDirection;
			if (shouldSpawnToRight()) {
				newEnemy = (GameObject)Instantiate(enemyPrefab, rightSpawnPoint.position, Quaternion.identity);
				runDirection = -1;
			}else {
				newEnemy = (GameObject)Instantiate(enemyPrefab, leftSpawnPoint.position, Quaternion.identity);
				runDirection = 1;
			}
			newEnemy.GetComponent<EnemyMovementController>().SetDirection(runDirection);				
			newEnemy.GetComponent<EnemyPunchingController>().movingRight = !lastSpawnRight;

		}
	}

	bool shouldSpawnToRight() {
		float max = 1.0f;
		float min = -1.0f;
		if (lastSpawnRight) {
			max = 0.5f;
		}else {
			min = -0.5f;
		}
		lastSpawnRight = Random.Range(min, max) > 0;
		return lastSpawnRight;
	}

}
