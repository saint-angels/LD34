﻿using UnityEngine;
using System.Collections;

public class LightsSpinner : MonoBehaviour {

	public float speed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward*speed);
	}
}
