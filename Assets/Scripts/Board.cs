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
}
