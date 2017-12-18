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
        component.id = _components.Count;
        _components.Add(component);
    }
    public void DeleteComponent(Component component) {
        _components.Remove(component);
    }
    public Component SearchComponent(Coord coord) {
        foreach (Component t in _components) {
            ComponentType componentType = ComponentType.GetComponentType(t.type);
            for (int i = 0; i < componentType.height; i++)
                if (t.coord.y + i == coord.y && t.coord.x == coord.x)
                    return t;
            for (int i = 0; i < componentType.width; i++)
                if (t.coord.x + i == coord.x && t.coord.y == coord.y)
                    return t;
        }
        throw new ArgumentException("Компонент не найден");
    }
    public void Add(ConnectionLine connectionLine) {
        _connections.Add(connectionLine);
    }
    public ConnectionLine SearchLine(Coord coord) {
        foreach (ConnectionLine t in _connections)
            for(int i = 0; i < t.intermediatePoints.Length; i++)
                if (t.intermediatePoints[i] == coord)
                    return t;
        throw new ArgumentException("Линия не найдена");
    }
    //Думаю это не надо уже
	//public Component GetComponent(){ return new Component (); }
	public void MoveComponent(){}
}
