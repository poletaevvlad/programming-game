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
[System.Serializable]
public class Component {
    public int id;
    public Coord coord;
    public ComponentTypeIndex type;
}
