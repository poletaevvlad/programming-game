using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(BoardModel))]
public class PointerMovementController : MonoBehaviour {

    public enum State {
        Normal, DrawingConnection
    }

    private State state = State.Normal;

    public Camera raycastCamera = null;
    private Transform previousHover = null;

    private ComponentIORenderer outputIORenderer = null;
    private ConnectionLineRenderer connectionLine = null;

    public ConnectionLineRenderer connectionLinePrefab;
    public int CurrentX;
    public int CurrentY;

    private BoardModel model;

    private void Start() {
        model = GetComponent<BoardModel>();
    }

    void Update () {
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        UpdatePoint(ray.origin);

        switch (state) {
            case State.Normal:
                HandleHovering(ray);
                break;
            case State.DrawingConnection:
                HandleDrawingConnection(ray);
                break;
        }
    }

    private int lineStartComponentId;
    private int lineStartConnectorIndex;

    private void HandleHovering(Ray screenRay) {
        Transform newHover = null;
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit)) {
            newHover = hit.transform;
        }
        ComponentIORenderer componentIORenderer;

        if (previousHover != newHover) {
            if (previousHover != null && (componentIORenderer = previousHover.GetComponent<ComponentIORenderer>()) != null) {
                componentIORenderer.HoverEnded();
            }
            if ((newHover != null) && (componentIORenderer = newHover.GetComponent<ComponentIORenderer>()) != null) {
                componentIORenderer.HoverStarted();
            }
            previousHover = newHover;
        }

        if (previousHover != null && (componentIORenderer = previousHover.GetComponent<ComponentIORenderer>()) != null) {
            if (!componentIORenderer.isInput && Input.GetKeyDown(KeyCode.Mouse0)) {
                outputIORenderer = componentIORenderer;
                outputIORenderer.Pressed();
                state = State.DrawingConnection;

                connectionLine = Instantiate(connectionLinePrefab, transform);
                Component component = outputIORenderer.transform.parent.GetComponent<ComponentModel>().component;
                connectionLine.startComponentId = lineStartComponentId = component.id;

                Connector connector = outputIORenderer.connector;
                connectionLine.startConnectorIndex = lineStartConnectorIndex = Array.IndexOf(ComponentType.GetComponentType(component.type).outputs, connector);
                connectionLine.Append(new Coord(CurrentX, CurrentY));
                RemoveConnection(component.id, lineStartConnectorIndex);
                switch (connector.direction) {
                    case ConnectorDirection.Left:
                        possibleNextMove = new Coord(CurrentX - 1, CurrentY);
                        break;
                    case ConnectorDirection.Right:
                        possibleNextMove = new Coord(CurrentX + 1, CurrentY);
                        break;
                    case ConnectorDirection.Up:
                        possibleNextMove = new Coord(CurrentX, CurrentY - 1);
                        break;
                    case ConnectorDirection.Down:
                        possibleNextMove = new Coord(CurrentX, CurrentY + 1);
                        break;
                }
            }
        }
    }

    private int GetComponentInputIndex(ComponentTypeIndex typeIndex, int x, int y, ConnectorDirection direction){
        ComponentType type = ComponentType.GetComponentType(typeIndex);
        for (int i = 0; i < type.inputs.Length; i++) {
            Connector connector = type.inputs[i];
            if(connector.x == x && connector.y == y && connector.direction == direction) {
                return i;
            }
        }
        return -1;
    }

    private Coord? possibleNextMove = null;
    private int lineEndComponentId = -1;
    private int lineEndConnectorIndex = -1;

    private void RemoveConnection(int startComponentId, int startConnectionIndex){
        bool connectionExists = false;
        for (int i = 0; i < model.board._connections.Count; i++) {
            ConnectionLine connectionLine = model.board._connections[i];
            if (connectionLine.startComponentId == startComponentId && connectionLine.startOutputIndex == startConnectionIndex) {
                connectionExists = true;
                model.board._connections.RemoveAt(i);
            }
        }
        if (connectionExists) {
            foreach (Transform child in transform.Cast<Transform>().ToArray()) {
                ConnectionLineRenderer lineRenderer = child.GetComponent<ConnectionLineRenderer>();
                if (lineRenderer != null && lineRenderer != connectionLine && lineRenderer.startComponentId == startComponentId && 
                    lineRenderer.startConnectorIndex == lineStartConnectorIndex) {
                    lineRenderer.Disconnect();
                }
            }
        }
    }

    private void HandleDrawingConnection(Ray screenRay) {
        Coord lastCell = connectionLine.GetLast();
        if ((lastCell.x != CurrentX || lastCell.y != CurrentY) && CurrentX >= 0 && CurrentY >= 0) {
            int x = lastCell.x, y = lastCell.y;
            while (x != CurrentX || y != CurrentY) {
                if (x < CurrentX) x++;
                else if (x > CurrentX) x--;
                else if (y < CurrentY) y++;
                else if (y > CurrentY) y--;
                if (possibleNextMove != null && (possibleNextMove.Value.x != x || possibleNextMove.Value.y != y)) {
                    break;
                }
                possibleNextMove = null;
                lineEndComponentId = lineEndConnectorIndex = -1;
                try {
                    Component component = model.board.SearchComponent(new Coord(x, y));
                    if (component.id == lineStartComponentId) break;
                    int connectorX = x - component.coord.x;
                    int connectorY = y - component.coord.y;
                    ConnectorDirection direction;
                    Coord last = connectionLine.GetLast();
                    if (last.x < x) direction = ConnectorDirection.Left;
                    else if (last.x > x) direction = ConnectorDirection.Right;
                    else if (last.y < y) direction = ConnectorDirection.Up;
                    else direction = ConnectorDirection.Down;
                    int connectorId = GetComponentInputIndex(component.type, connectorX, connectorY, direction);
                    if (connectorId == -1) {
                        break;
                    } else {
                        connectionLine.Append(x, y);
                        possibleNextMove = last;
                        lineEndComponentId = component.id;
                        lineEndConnectorIndex = connectorId;
                    }
                } catch {
                    try {
                        model.board.SearchLine(new Coord(x, y));
                        break;
                    } catch (ArgumentException) {
                        connectionLine.Append(x, y);
                    }
                }
            }   
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            RaycastHit raycast;
            if (!outputIORenderer.GetComponent<Collider>().Raycast(screenRay, out raycast, 1000)) {
                outputIORenderer.HoverEnded();
            }
            outputIORenderer.Released();
            state = State.Normal;
            possibleNextMove = null;

            if (lineEndComponentId >= 0 && lineEndConnectorIndex >= 0) {
                ConnectionLine line = new ConnectionLine();
                line.startComponentId = lineStartComponentId;
                line.startOutputIndex = lineStartConnectorIndex;
                line.endComponentId = lineEndComponentId;
                line.endOutputIndex = lineEndConnectorIndex;
                line.intermediatePoints = new Coord[connectionLine.coordinates.Count];
                for (int i = 0; i < connectionLine.coordinates.Count; i++) {
                    line.intermediatePoints[i] = connectionLine.coordinates[i];
                }
                model.board.Add(line);
            } else {
                Destroy(connectionLine.gameObject);
            }
        }
    }

    private void UpdatePoint(Vector3 position) {
        CurrentX = (int)(position.x + model.board.width / 2f);
        CurrentY = (int)(model.board.heigth - position.y - model.board.heigth / 2f);
        if (CurrentX < 0 || CurrentX >= model.board.width || CurrentY < 0 || CurrentY >= model.board.heigth) {
            CurrentX = CurrentY = -1;
        } 
    }
}
