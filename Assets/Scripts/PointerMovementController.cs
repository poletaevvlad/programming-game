using UnityEngine;
using UnityEngine.UI;

public class PointerMovementController : MonoBehaviour {

    public Camera raycastCamera = null;

    private Transform previousHover = null;

	void Update () {
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

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
}
