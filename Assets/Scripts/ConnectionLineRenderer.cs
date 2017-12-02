using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class ConnectionLineRenderer : MonoBehaviour {

    public Coord[] coordinates;

    private BoardResizer _boardResizer = null;
    public BoardResizer boardResizer {
        get {
            if (_boardResizer == null) {
                _boardResizer = GameObject.Find("/Field/Board").GetComponent<BoardResizer>();
            }
            return _boardResizer;
        }
    }

    public void UpdateLine() {
        List<Vector3> vector = new List<Vector3>();
        foreach (Coord coord in coordinates) {
            Vector3 position = new Vector3(boardResizer.GetLeft(coord.x) + boardResizer.CellWidth / 2, 
                                           boardResizer.GetTop(coord.y) + boardResizer.CellHeight / 2);
            vector.Add(position);
        }
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vector.Count;
        lineRenderer.SetPositions(vector.ToArray());
    }


}
