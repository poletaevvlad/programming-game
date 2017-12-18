using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ComponentModel))]
public class ComponentGenerator : MonoBehaviour {
    public IOBuilderParameters parameters;

    private BoardResizer boardResizer = null;
    public Text label;
    public Canvas canvas;

    private Component _component = null;
    private Component component {
        get {
            if (_component == null) {
                _component = GetComponent<ComponentModel>().component;
            }
            return _component;
        }
    }

    private ComponentType _componentType;
    private ComponentType componentType {
        get {
            if (_componentType == null) {
                _componentType = ComponentType.GetComponentType(component.type);
            }
            return _componentType;
        }
    }


    private void RequestResizer(){
        if (boardResizer == null) {
            boardResizer = GameObject.Find("/Field/Board").GetComponent<BoardResizer>();
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
        float xPosition = boardResizer.GetLeft(component.coord.x) + boardResizer.CellWidth * componentType.width / 2;
        float yPosition = boardResizer.GetTop(component.coord.y) - boardResizer.CellHeight * componentType.height / 2;
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(componentType.width * boardResizer.CellWidth, componentType.height * boardResizer.CellHeight);
    }
    
    private static ConnectorDirection ReverseDirection(ConnectorDirection direction){
        switch (direction) {
            case ConnectorDirection.Left:
                return ConnectorDirection.Right;
            case ConnectorDirection.Right:
                return ConnectorDirection.Left;
            case ConnectorDirection.Up:
                return ConnectorDirection.Down;
            default:
                return ConnectorDirection.Up;
        }
    }

    private void InstantiateCircle(int r, int c, ConnectorDirection direction, bool isInput, Connector connector){
        Transform newObject = Instantiate(parameters.circlePrefab, transform);
        ComponentIORenderer renderer = newObject.GetComponent<ComponentIORenderer>();
        renderer.isInput = isInput;
        renderer.connector = connector;
        float xOffset = !(direction == ConnectorDirection.Left || direction == ConnectorDirection.Right) ? 0 : (1 - (componentType.width % 2)) * 1f / 64f;
        float yOffset = !(direction == ConnectorDirection.Left || direction == ConnectorDirection.Right) ? (1 - (componentType.height % 2)) * 1f / 64f : 0;
        newObject.localPosition = parameters.GetCirclePosition(direction) + new Vector3(c - componentType.width / 2f + xOffset + 0.5f, -r + componentType.height / 2f + yOffset - 0.5f);
        if (isInput) {
            direction = ReverseDirection(direction);
            Transform arrow = Instantiate(parameters.arrowPrefab, newObject);
            arrow.localPosition = parameters.GetArrowPosition(direction);
            arrow.rotation = Quaternion.Euler(0, 0, parameters.GetArrowRotation(direction));
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
        foreach (Connector input in componentType.inputs) {
            InstantiateCircle(input.y, input.x, input.direction, true, input);
        }

        foreach (Connector output in componentType.outputs) {
            InstantiateCircle(output.y, output.x, output.direction, false, output);
        }

        label.text = componentType.label;
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(32 * componentType.width, 32 * componentType.height);
        canvas.transform.localPosition = new Vector3(componentType.width % 2 == 0 ? 1f/64f : 0, componentType.height % 2 == 0 ? 1f / 64f : 0, 1);
    }

}
