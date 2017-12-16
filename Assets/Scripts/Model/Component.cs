using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public enum ComponentTypeIndex{
	Addition,
	Conditional,
	LineSplit,
	LineIntersection,
    Memory
}

public class Component {
    int id { get; set;}
    public Coord coord { get; set; }
	public ComponentTypeIndex type { get; set;}
}
