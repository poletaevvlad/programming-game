using UnityEngine;

[RequireComponent(typeof(BoardModel))]
public class PointerMovementController : MonoBehaviour {

    public Camera raycastCamera = null;

    private Transform previousHover = null;

    public int CurrentX;
    public int CurrentY;

    private BoardModel model;

    private void Start() {
        model = GetComponent<BoardModel>();
    }

    void Update () {
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        UpdatePoint(ray.origin);

        Transform newHover = null;
        if (Physics.Raycast(ray, out hit)) {
            newHover = hit.transform;
        }

        if (previousHover != newHover) {
            ComponentIORenderer componentIORenderer;
            if (previousHover != null && (componentIORenderer = previousHover.GetComponent<ComponentIORenderer>()) != null) {
                componentIORenderer.HoverEnded();
            }
            if ((newHover != null) && (componentIORenderer = newHover.GetComponent<ComponentIORenderer>()) != null) {
                componentIORenderer.HoverStarted();
            }
            previousHover = newHover;
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
