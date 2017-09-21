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
}
