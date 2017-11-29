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
    
    private void InstantiateCircle(int r, int c, Vector3 relativePosition, Vector3 arrowPosition, float rotation, bool horizontal){
        Transform newObject = Instantiate(parameters.circlePrefab, transform);
        float xOffset = !horizontal ? 0 : (1 - (width % 2)) * 1f / 64f;
        float yOffset = !horizontal ? (1 - (height % 2)) * 1f / 64f : 0;
        newObject.localPosition = relativePosition + new Vector3(c - (width) / 2f + xOffset + 0.5f, -r + (height) / 2f + yOffset - 0.5f);
        if (arrowPosition != null) {
            Transform arrow = Instantiate(parameters.arrowPrefab, newObject);
            arrow.localPosition = arrowPosition;
            arrow.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    [ContextMenu("Clear content")]
    public void Clear(){
        while (transform.childCount > 0) {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    [ContextMenu("Generate content")]
    public void Generate() {
        // Todo: Добавить проверку на существование ввода/вывода
        for (int x = 0; x < width; x++) {
            InstantiateCircle(0, x, parameters.topCirclePosition, parameters.topArrowPosition, parameters.topArrowRotation, true);
            InstantiateCircle(height - 1, x, parameters.bottomCirclePosition, parameters.bottomArrowPosition, parameters.bottomArrowRotation, true);
        }
        for (int y = 0; y < height; y++) {
            InstantiateCircle(y, 0, parameters.leftCirclePosition, parameters.leftArrowPosition, parameters.leftArrowRotation, false);
            InstantiateCircle(y, width - 1, parameters.rightCirclePosition, parameters.rightArrowPosition, parameters.rightArrowRotation, false);
        }
    }

}
