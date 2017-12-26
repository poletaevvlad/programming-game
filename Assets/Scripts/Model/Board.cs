using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Board {
    public List<Component> _components;
    public List<ConnectionLine> _connections;
    private int[,] coord;

    [SerializeField]
    public int width;

    [SerializeField]
    public int heigth;

    public void AddComponent(Component component) {
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

    private bool PointIntersectsLine(Coord coord, ConnectionLine t){
        for (int i = 0; i < t.intermediatePoints.Length - 1; i++) {
            if ((
                (t.intermediatePoints[i].x <= coord.x && t.intermediatePoints[i + 1].x >= coord.x) ||
                (t.intermediatePoints[i].x >= coord.x && t.intermediatePoints[i + 1].x <= coord.x)
                ) && (
                (t.intermediatePoints[i].y <= coord.y && t.intermediatePoints[i + 1].y >= coord.y) ||
                (t.intermediatePoints[i].y >= coord.y && t.intermediatePoints[i + 1].y <= coord.y)
                )) {
                return true;
            }
        }
        return false;
    }

    public ConnectionLine SearchLine(Coord coord) {
        foreach (ConnectionLine t in _connections)
            if (PointIntersectsLine(coord, t)) return t;
        throw new ArgumentException("Линия не найдена");
    }

    public bool CanPlaceComponent(int width, int height, Coord coord, int ignoreId) {
        if (coord.x < 0 || coord.y < 0 || coord.x + width >= this.width || coord.y + height >= this.heigth) {
            return false;
        }

        foreach(Component component in _components) {
            if (component.id != ignoreId) {
                Coord aLT, aRB, bLT, bRB;
                ComponentType type = ComponentType.GetComponentType(component.type);
                if (component.coord.x < coord.x) {
                    aLT = component.coord;
                    aRB = new Coord(aLT.x + type.width - 1, aLT.y + type.height - 1);
                    bLT = coord;
                    bRB = new Coord(bLT.x + width - 1, bLT.y + height - 1);
                } else {
                    aLT = coord;
                    aRB = new Coord(aLT.x + width - 1, aLT.y + height - 1);
                    bLT = component.coord;
                    bRB = new Coord(bLT.x + type.width - 1, bLT.y + type.height - 1);
                }

                if (!(aLT.x > bRB.x || aRB.x < bLT.x || aLT.y > bRB.y || aRB.y < bLT.y)) {
                    return false;
                }
            }
        }

        foreach (ConnectionLine line in _connections) {
            if (line.startComponentId != ignoreId && line.endComponentId != ignoreId) {
                for (int x = coord.x; x < coord.x + width; x++) {
                    for (int y = coord.y; y < coord.y + height; y++) {
                        if (PointIntersectsLine(new Coord(x, y), line)) {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    //Думаю это не надо уже
	//public Component GetComponent(){ return new Component (); }
	public void MoveComponent(){}
}
