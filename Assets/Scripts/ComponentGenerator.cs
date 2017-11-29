using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentGenerator : MonoBehaviour {

    public int column;
    public int row;
    public int width;
    public int height;
    public IOBuilderParameters parameters;

    private BoardResizer boardResizer = null;

    private void RequestResizer(){
        if (boardResizer == null) {
            boardResizer = GameObject.Find("/Board").GetComponent<BoardResizer>();
        }
    }

    [ContextMenu("Update all")]
    public void UpdateAll() {
        Position();
        Clear();
        Generate();
    }

    [ContextMenu("Update transform")]
    public void Position() {
        RequestResizer();
        float xPosition = boardResizer.GetLeft(column) + boardResizer.CellWidth * width / 2;
        float yPosition = boardResizer.GetTop(row) - boardResizer.CellHeight * height / 2;
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(width * boardResizer.CellWidth, height * boardResizer.CellHeight);
    }
    
    private void InstantiateCircle(int r, int c, Vector3 relativePosition){
        Transform newObject = Instantiate(parameters.circlePrefab, transform);
        newObject.localPosition = relativePosition + new Vector3(c - (width - 1) / 2f, -r + (height - 1) / 2f);
    }
    
    [ContextMenu("Clear content")]
    public void Clear(){
        while (transform.childCount > 0) {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    [ContextMenu("Generate content")]
    public void Generate() {
        for (int x = 0; x < width; x++) {
            InstantiateCircle(0, x, parameters.topCirclePosition);
            InstantiateCircle(height - 1, x, parameters.bottomCirclePosition);
        }
        for (int y = 0; y < height; y++) {
            InstantiateCircle(y, 0, parameters.leftCirclePosition);
            InstantiateCircle(y, width - 1, parameters.rightCirclePosition);
        }
    }

}
