using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectorDirection{
	Left,
	Right,
	Up,
	Down
}

public class Connector {

	public int x { get; set;}
	public int y { get; set;}
	public ConnectorDirection direction { get; set;}
}
