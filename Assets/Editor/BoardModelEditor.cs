using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardModel))]
public class BoardModelEditor : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		if (GUILayout.Button ("Random")) {
			BoardModel model = target as BoardModel;
			model.GenerateRandom ();
		}
	}

}
