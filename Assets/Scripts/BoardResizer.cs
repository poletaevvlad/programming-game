using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BoardResizer : MonoBehaviour {

	public Camera mainCamera;
		
	[Header("Paddings")]
	public float leftPadding = 0;
	public float topPadding = 0;
	public float rightPadding = 0;
	public float bottomPadding = 0;

	void Start () {
		if (mainCamera == null) {
			Debug.LogError ("Main camera property must be set for BoardResizer script");
		}
	}
		
	private float scaleToHeight(float targetHeight, float padding){
		return Screen.height * (targetHeight / 2f) / (Screen.height - padding);
	}

	private float scaleToWidth(float targetWidth, float padding, float aspectRatio){
		return Screen.width * (targetWidth / 2f) / (Screen.width - padding) / aspectRatio;
	}

	public void Resize () {
		float targetWidth = transform.localScale.x;
		float targetHeight = transform.localScale.y;

		float orthographicSize = Mathf.Max (scaleToHeight (targetHeight, topPadding + bottomPadding), 
			                         scaleToWidth (targetWidth, leftPadding + rightPadding, mainCamera.aspect));
		mainCamera.orthographicSize = orthographicSize;

		Vector3 topLeftViewportPoint = mainCamera.ViewportToWorldPoint (new Vector3 (0, 0));
		Vector3 bottomRightViewportPoint = mainCamera.ViewportToWorldPoint (new Vector3 (1, 1));
		float viewportWidth = bottomRightViewportPoint.x - topLeftViewportPoint.x;
		float viewportHeight = bottomRightViewportPoint.y - topLeftViewportPoint.y;

		Vector2 onScreenOrigin = new Vector2 ((Screen.width + leftPadding - rightPadding) / 2 / Screen.width, 
			(Screen.height + topPadding - bottomPadding) / 2 / Screen.height);
		Vector3 cameraPosition = new Vector3 (-viewportWidth * (onScreenOrigin.x - 0.5f),
			viewportHeight * (onScreenOrigin.y - 0.5f), mainCamera.transform.position.z);
		mainCamera.transform.position = cameraPosition;
	}
}
