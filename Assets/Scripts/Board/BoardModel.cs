 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class BoardModel : MonoBehaviour {
	
	public Board board;

	public UnityEvent rebuildRequiredEvent;

    public void Rebuild() {
        rebuildRequiredEvent.Invoke();
    }

    public ConnectionLineRenderer FindLineRenderer(ConnectionLine line){
        foreach(Transform child in transform) {
            ConnectionLineRenderer lineRenderer = child.GetComponent<ConnectionLineRenderer>();
            if (lineRenderer != null && lineRenderer.startComponentId == line.startComponentId && 
                lineRenderer.startConnectorIndex == line.startOutputIndex) {
                return lineRenderer;
            }
        }
        return null;
    }
}
