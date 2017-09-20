using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BoardRenderer : MonoBehaviour {

	public void SetSize(int width, int height){
		Material mat = GetComponent<MeshRenderer> ().sharedMaterial;
		mat.SetTextureScale ("_MainTex", new Vector2 (width + 1f / 32f, height + 1f / 32f));
		float maxDimension = Mathf.Max (transform.localScale.x, transform.localScale.y);
		mat.SetFloat ("_ObjectXScale", transform.localScale.x / maxDimension);
		mat.SetFloat ("_ObjectYScale", transform.localScale.y / maxDimension);
	}

}
