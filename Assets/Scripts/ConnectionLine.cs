using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLine {
	int startComponentId { get; set;}
	int startOutputIndex { get; set;}
	int endComponentId { get; set;}
	int endOutputIndex { get; set;}
	public Coord intermediatePoints { get; set;}
}
