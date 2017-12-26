using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoardModel))]
public class FieldInitializator : MonoBehaviour {

    private BoardModel model = null;
    public Transform componentPrefab;
    public Transform connectionPrefab;

    public void InitializeBoard() {
        if (model == null) {
            model = GetComponent<BoardModel>();
        }
        
        // Clearing field hierarchy
        foreach (Transform child in transform.Cast<Transform>().ToArray()) {
            if (child.name != "Board") {
                if (Application.isPlaying) {
                    Destroy(child.gameObject);
                } else {
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        // Adding components
        foreach (Component component in model.board._components) {
            AddComponent(component);
        }

        // Adding connection lines
        foreach (ConnectionLine line in model.board._connections) {
            AddConnectionLine(line);
        }
    }

    private void AddComponent(Component component){
        Transform newTransform = Instantiate(componentPrefab, transform);
        newTransform.GetComponent<ComponentModel>().component = component;
        ComponentGenerator generator = newTransform.GetComponent<ComponentGenerator>();
        generator.Position();
        generator.Generate();
    }

    private void AddConnectionLine(ConnectionLine connectionLine){
        Transform newTransform = Instantiate(connectionPrefab, transform);
        ConnectionLineRenderer lineRenderer = newTransform.GetComponent<ConnectionLineRenderer>();
        foreach (Coord coord in connectionLine.intermediatePoints) {
            lineRenderer.Append(coord);
        }
        lineRenderer.startComponentId = connectionLine.startComponentId;
        lineRenderer.startConnectorIndex = connectionLine.startOutputIndex;
        lineRenderer.endComponentId = connectionLine.endComponentId;
        lineRenderer.CancelAnimation();
    }

    public void Start() {
        InitializeBoard();
    }

    public void DetachConnections(int component){
        model.board._connections.RemoveAll((ConnectionLine line) => line.startComponentId == component || line.endComponentId == component);
        foreach (Transform child in transform.Cast<Transform>().ToArray()) {
            ConnectionLineRenderer lineRenderer = child.GetComponent<ConnectionLineRenderer>();
            if (lineRenderer != null) {
                if (lineRenderer.startComponentId == component){
                    lineRenderer.Disconnect(false);
                } else if (lineRenderer.endComponentId == component) {
                    lineRenderer.Disconnect(true);
                }
            }
        }
    }

}
