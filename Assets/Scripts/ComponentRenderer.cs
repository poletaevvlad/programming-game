using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentRenderer : MonoBehaviour {

    public int x;
    public int y;
    public int w;
    public int h;

    private BoardResizer boardResizer = null;

    private void RequestResizer() {
        if (boardResizer == null) {
            boardResizer = GameObject.Find("/Board").GetComponent<BoardResizer>();
        }
    }

    [ContextMenu("Update transform")]
    public void Position() {
        RequestResizer();
        float xPosition = boardResizer.GetLeft(x) + boardResizer.CellWidth * w / 2;
        float yPosition = boardResizer.GetTop(y) - boardResizer.CellHeight * h / 2;
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(w * boardResizer.CellWidth, h * boardResizer.CellHeight);
    }
}
