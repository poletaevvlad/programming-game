using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Board {

	Component[] component { get; set;}

	[SerializeField]
	public int width;

	[SerializeField]
	public int heigth;

	ConnectionLine connections { get; set;}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
