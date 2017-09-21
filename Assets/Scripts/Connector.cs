using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ConnectorDirection{
	Left,
	Right,
	Up,
	Down
}

public class Connector {

	int x { get; set;}
	int y { get; set;}
	ConnectorDirection direction { get; set;}
}
