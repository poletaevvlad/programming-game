using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Board {
	private List<Component> _components { get; set;}
	private List<ConnectionLine> _connections { get; set;}
	private int[,] coord;

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

	public void AddComponent(Component component){
        _components.Add(component);
    }
    public void DeleteComponent(Component component) {
        _components.Remove(component);
    }
    public Component SearchComponent(Coord coord) {
        foreach (Component t in _components)
            if (t.coord == coord)
                return t;
        throw new ArgumentException("Компонент не найден");
    }
    public ConnectionLine SearchLine(Coord coord) {
        foreach (ConnectionLine t in _connections)
            if (t.intermediatePoints == coord)
                return t;
        throw new ArgumentException("Линия не найдена");
    }
    //Думаю это не надо уже
	//public Component GetComponent(){ return new Component (); }
	public void MoveComponent(){}
}
