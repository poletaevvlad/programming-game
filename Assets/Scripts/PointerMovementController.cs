using UnityEngine;

[RequireComponent(typeof(BoardModel))]
public class PointerMovementController : MonoBehaviour {

    public enum State {
        Normal, DrawingConnection
    }

    private State state = State.Normal;

    public Camera raycastCamera = null;
    private Transform previousHover = null;
    private ComponentIORenderer outputIORenderer = null;

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
            }
        }
    }

    private void HandleDrawingConnection(Ray screenRay) {
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            RaycastHit raycast;
            if (!outputIORenderer.GetComponent<Collider>().Raycast(screenRay, out raycast, 1000)) {
                outputIORenderer.HoverEnded();
            }
            outputIORenderer.Released();
            state = State.Normal;
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
