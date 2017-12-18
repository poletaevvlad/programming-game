using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public enum ComponentTypeIndex{
	Addition,
    Negative,
    Multiplication,
    Inverse,
    Memory,
    Conditional,
    Value,
    Increment,
    Decrement
}

public class Component {
    public int id { get; set;}
    public Coord coord { get; set; }
	public ComponentTypeIndex type { get; set;}
}
