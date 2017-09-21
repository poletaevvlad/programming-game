using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Board {

	private List<Component> _components { get; set;}
	private List<ConnectionLine> _connections { get; set;}

	[SerializeField]
	public int width;

	[SerializeField]
	public int heigth;

	[SerializeField]
	public Component[] components{
		get{ 
			return _components.ToArray ();
		}
		set {
			_components = new List<Component> (value);
		}
	}

	[SerializeField]
	public ConnectionLine[] coonnections{
		get{
			return _connections.ToArray ();
		}
		set{
			_connections = new List<ConnectionLine> (value);
		}
	}
}
