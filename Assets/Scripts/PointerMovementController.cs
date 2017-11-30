using UnityEngine;
using UnityEngine.UI;

public class PointerMovementController : MonoBehaviour {

    public Camera raycastCamera = null;

	void Update () {
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        string name = "-- empty --";
        if (Physics.Raycast(ray, out hit)) {
            name = hit.transform.gameObject.ToString();
        }
        GameObject.Find("/Canvas/Debug text").GetComponent<Text>().text = name;
    }
}
