using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardModel))]
public class BoardModelEditor : Editor {

	private int width, height;
	private bool initialized = false;

	public override void OnInspectorGUI(){
		BoardModel model = target as BoardModel;
		if (!initialized && model.board != null) {
			width = model.board.width;
			height = model.board.heigth;
			initialized = true;
		}
			
		DrawDefaultInspector ();
		if (GUILayout.Button ("Random")) {
			model.GenerateRandom ();
		}

		if (model.board != null) {
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("w:");
			width = EditorGUILayout.IntField (width);
			GUILayout.Label ("h:");
			height = EditorGUILayout.IntField (height);
			if (GUILayout.Button ("Resize")) {
				model.Resize (width, height);
			}
			GUILayout.EndHorizontal ();
		}
	}
		

}
