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
    private Coord _coord;
    public Coord coord { get { return _coord; } set { _coord = value; Debug.Log("coord set"); } }
	public ComponentTypeIndex type { get; set;}
}
