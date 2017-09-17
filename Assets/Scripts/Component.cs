using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public enum ComponentTypeIndex{
	Addition,
	Conditional,
	LineSplit,
	LineIntersection
}

public class Component {

	int id { get; set;}
	Coord coord { get; set;}
	ComponentTypeIndex type { get; set;}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
