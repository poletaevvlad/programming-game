using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ConnectionLineRenderer))]
class ConnectionLineRendererEditor: Editor {
    
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        ConnectionLineRenderer renderer = (ConnectionLineRenderer)target;
        if (GUILayout.Button("Rebuild")) {
            renderer.UpdateLine();
        }
    }

}
