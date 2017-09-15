using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardResizer))]
public class BoardResizerEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();
		if(GUILayout.Button("Resize")){
			((BoardResizer)target).Resize ();
		}
	}
}
