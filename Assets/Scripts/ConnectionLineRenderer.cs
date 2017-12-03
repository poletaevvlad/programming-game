using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class ConnectionLineRenderer : MonoBehaviour {

    public Coord[] coordinates;
    private int length;

    public float width;
    public float skipStartDist;
    public float skipEndDist;

    private BoardResizer _boardResizer = null;
    public BoardResizer boardResizer {
        get {
            if (_boardResizer == null) {
                _boardResizer = GameObject.Find("/Field/Board").GetComponent<BoardResizer>();
            }
            return _boardResizer;
        }
    }

    private Vector3 CoordToVec3(Coord coord){
        return new Vector3(boardResizer.GetLeft(coord.x) + boardResizer.CellWidth / 2,
                           boardResizer.GetTop(coord.y) - boardResizer.CellHeight / 2);
    }

    public void UpdateLine()
    {
        float distSqrt2 = width * Mathf.Sqrt(2);

        List<Vector3> vertices = new List<Vector3>();
        int i = 0;
        float distance = skipEndDist;
        float skipLength = Mathf.Max(skipStartDist, 0);
        while (i < coordinates.Length - 1) {
            float segmentDistance = ManhattanDistance(coordinates[i], coordinates[i + 1]);
            distance -= segmentDistance;
            if (skipLength < segmentDistance) {
                break;
            } else {
                skipLength -= segmentDistance;
                i++;
            }
        }
        if (i < coordinates.Length - 1) {
            float angle = Mathf.Atan2(coordinates[i].y - coordinates[i + 1].y, -coordinates[i].x + coordinates[i + 1].x);
            int currentDist = ManhattanDistance(coordinates[i], coordinates[i + 1]);
            float lerpFactor = skipLength / currentDist;
            distance += currentDist;
            Vector3 point = Vector3.Lerp(CoordToVec3(coordinates[i]), CoordToVec3(coordinates[i + 1]), lerpFactor);
            vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle - 3 * Mathf.PI / 4), Mathf.Sin(angle - 3 * Mathf.PI / 4)));
            vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle + 3 * Mathf.PI / 4), Mathf.Sin(angle + 3 * Mathf.PI / 4)));
            for (i++; i < coordinates.Length - 1; i++) {
                float segmentLength = ManhattanDistance(coordinates[i], coordinates[i - 1]);
                if (distance < segmentLength) {
                    i--;
                    break;
                }
                distance -= segmentLength;
                float bAngle = Mathf.Atan2(coordinates[i].y - coordinates[i + 1].y, -coordinates[i].x + coordinates[i + 1].x);
                point = CoordToVec3(coordinates[i]);
                vertices.Add(point + width / 2f / Mathf.Pow(Mathf.Cos((bAngle - angle) / 2), 2) *
                                              new Vector3(Mathf.Cos(angle - Mathf.PI / 2) + Mathf.Cos(bAngle - Mathf.PI / 2),
                                                          Mathf.Sin(angle - Mathf.PI / 2) + Mathf.Sin(bAngle - Mathf.PI / 2)));
                vertices.Add(point + width / 2f / Mathf.Pow(Mathf.Cos((bAngle - angle) / 2), 2) *
                                              new Vector3(Mathf.Cos(angle + Mathf.PI / 2) + Mathf.Cos(bAngle + Mathf.PI / 2),
                                                          Mathf.Sin(angle + Mathf.PI / 2) + Mathf.Sin(bAngle + Mathf.PI / 2)));
                angle = bAngle;
            }

            if (i == coordinates.Length - 1) {
                i--;
            }
            lerpFactor = distance / ManhattanDistance(coordinates[i], coordinates[i + 1]);
            point = Vector3.Lerp(CoordToVec3(coordinates[i]), CoordToVec3(coordinates[i + 1]), lerpFactor);
            vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle + 7 * Mathf.PI / 4), Mathf.Sin(angle + 7 * Mathf.PI / 4)));
            vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle - 7 * Mathf.PI / 4), Mathf.Sin(angle - 7 * Mathf.PI / 4)));
            int[] triangles = new int[3 * vertices.Count - 6];

            for (int j = 0; j < vertices.Count - 2; j += 2) {
                triangles[3 * j + 0] = j;
                triangles[3 * j + 1] = triangles[3 * j + 4] = j + 1;
                triangles[3 * j + 2] = triangles[3 * j + 3] = j + 2;
                triangles[3 * j + 5] = j + 3;
            }

            Mesh mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            GetComponent<MeshFilter>().mesh = mesh;
        } else {
            GetComponent<MeshFilter>().mesh = null;
        }
    }

    public void Update() {
        UpdateLine();
    }

    private void OnValidate(){
        if (skipStartDist < 0) skipStartDist = 0;
        if (skipEndDist < 0) skipEndDist = 0;
    }

    private static int ManhattanDistance(Coord a, Coord b){
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.black; 
        for (int i = 1; i < coordinates.Length; i++) {
            Gizmos.DrawLine(CoordToVec3(coordinates[i - 1]), CoordToVec3(coordinates[i]));
        }
    }

}
