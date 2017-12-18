using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLine {
    public int startComponentId { get; set;}
    public int startOutputIndex { get; set;}
    public int endComponentId { get; set;}
    public int endOutputIndex { get; set;}
	public Coord[] intermediatePoints { get; set;}
}
