using UnityEditor;

[CustomEditor(typeof(ComponentIORenderer))]
class ComponentIORendererEditor: Editor{

    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        ComponentIORenderer renderer = (ComponentIORenderer)target;
        renderer.Radius = EditorGUILayout.FloatField("Radius", renderer.Radius);
    }

}