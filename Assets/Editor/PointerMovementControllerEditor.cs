using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(PointerMovementController))]
class PointerMovementControllerEditor : Editor {

    public override void OnInspectorGUI() {
        PointerMovementController controller = (PointerMovementController)target;
        controller.raycastCamera = (Camera)EditorGUILayout.ObjectField("Raycast Camera", controller.raycastCamera, typeof(Camera), true);

        EditorGUILayout.LabelField("Column (y):", controller.CurrentY.ToString());
        EditorGUILayout.LabelField("Row (x):", controller.CurrentX.ToString()); 
    }

}
