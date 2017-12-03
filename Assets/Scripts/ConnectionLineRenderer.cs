using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class ConnectionLineRenderer : MonoBehaviour {

    public Coord[] coordinates;
    private int length;

    public float width;
    public float visibleDist;

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

    private List<Vector3> vertices;
    private int[] triangles;

    public void UpdateLine() {
        vertices = new List<Vector3>();

        float distSqrt2 = width * Mathf.Sqrt(2);

        float angle = Mathf.Atan2(coordinates[0].y - coordinates[1].y, -coordinates[0].x + coordinates[1].x);
        Vector3 point = CoordToVec3(coordinates[0]);
        vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle - 3 * Mathf.PI / 4), Mathf.Sin(angle - 3 * Mathf.PI / 4)));
        vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle + 3 * Mathf.PI / 4), Mathf.Sin(angle + 3 * Mathf.PI / 4)));
        for (int i = 1; i < coordinates.Length - 1; i++) {
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
        point = CoordToVec3(coordinates[coordinates.Length - 1]);
        vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle + 7 * Mathf.PI / 4), Mathf.Sin(angle + 7 * Mathf.PI / 4)));
        vertices.Add(point + distSqrt2 * new Vector3(Mathf.Cos(angle - 7 * Mathf.PI / 4), Mathf.Sin(angle - 7 * Mathf.PI / 4)));

        triangles = new int[3 * vertices.Count - 6];
        for (int i = 0; i < vertices.Count - 2; i += 2) {
            triangles[3 * i + 0] = i;
            triangles[3 * i + 1] = triangles[3 * i + 4] = i + 1;
            triangles[3 * i + 2] = triangles[3 * i + 3] = i + 2;
            triangles[3 * i + 5] = i + 3;
        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void Update(){
    }

    private static int ManhattanDistance(Coord a, Coord b){
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.black; 
        for (int i = 1; i < coordinates.Length; i++) {
            Gizmos.DrawLine(CoordToVec3(coordinates[i - 1]), CoordToVec3(coordinates[i]));
        }

        Gizmos.color = Color.blue;
        foreach (Vector3 vec in vertices) {
            Gizmos.DrawSphere(vec, 0.05f);
        }
        for (int i = 0; i < triangles.Length; i+= 3) {
            Gizmos.DrawLine(vertices[triangles[i]], vertices[triangles[i + 1]]);
            Gizmos.DrawLine(vertices[triangles[i]], vertices[triangles[i + 2]]);
            Gizmos.DrawLine(vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
        }
    }

}
