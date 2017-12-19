using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExecutionManager))]
public class ExecutionManagerEditor : Editor {

    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            ExecutionManager manager = (ExecutionManager)target;
            if (GUILayout.Button("Step")) {
                manager.ComputeStep();
            }
            if (GUILayout.Button("Break")) {
                manager.Break();
            }
        }
    }

}
