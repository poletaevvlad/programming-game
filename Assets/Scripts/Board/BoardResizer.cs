﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(BoardRenderer))]
[RequireComponent(typeof(BoardModel))]
public class BoardResizer : MonoBehaviour {

	public Camera mainCamera;
		
	[Header("Paddings")]
	public float leftPadding = 0;
	public float topPadding = 0;
	public float rightPadding = 0;
	public float bottomPadding = 0;

	/* Storing screen dimensions in order to detect its change */
	private int screenWidth = 0;
	private int screenHeight = 0;

	void Start () {
		if (mainCamera == null) {
			Debug.LogError ("Main camera property must be set for BoardResizer script");
		}
	}

	void Update(){
		if (Screen.width != screenWidth || Screen.height != screenHeight) {
			screenWidth = Screen.width;
			screenHeight = Screen.height;
			Resize ();
		}
	}

	void OnValidate(){
		// Reseting screen width. Resize() will be called on the next Update.
		screenWidth = 0;
	}
		
    [ContextMenu("Resize")]
	public void Resize () {
        BoardModel boardModel = GetComponentInParent<BoardModel>();
		//if (boardModel == null || boardModel.board == null)
		//	return;
		int width = boardModel.board.width, height = boardModel.board.heigth;
		transform.localScale = new Vector3(width + 1f / 32f, height + 1f / 32f, 1);
		GetComponent<BoardRenderer> ().SetSize (width, height);

		//TODO: Add additional grid texture sizes
		float scale = screenHeight / 32f / 2f;
		mainCamera.orthographicSize = scale;
		mainCamera.transform.localPosition = new Vector3 (-1f / 64, -1f / 64, -10);

		Vector3 topLeftViewportPoint = mainCamera.ViewportToWorldPoint (new Vector3 (0, 0));
		Vector3 bottomRightViewportPoint = mainCamera.ViewportToWorldPoint (new Vector3 (1, 1));
		float viewportWidth = bottomRightViewportPoint.x - topLeftViewportPoint.x;
		float viewportHeight = bottomRightViewportPoint.y - topLeftViewportPoint.y;

		Vector2 onScreenOrigin = new Vector2 ((screenWidth + leftPadding - rightPadding) / 2 / screenWidth, 
			(screenHeight + topPadding - bottomPadding) / 2 / screenHeight);
		Vector3 cameraPosition = new Vector3 (-viewportWidth * (onScreenOrigin.x - 0.5f),
			viewportHeight * (onScreenOrigin.y - 0.5f), mainCamera.transform.position.z);
		cameraPosition.x = (int)(cameraPosition.x * 32f) / 32f + (1 - screenWidth %	 2) / 64f;
		cameraPosition.y = (int)(cameraPosition.y * 32f) / 32f + (1 - screenHeight % 2) / 64f;
		mainCamera.transform.position = cameraPosition;
	}

    public float GetLeft(int column){
        return -transform.localScale.x / 2f + column;
    }

    public float GetTop(int row){
        return transform.localScale.y / 2f - row;
    }

    public float CellWidth {
        get { return 1 + 1f / 32f; }
    }

    public float CellHeight {
        get { return 1 + 1f / 32f; }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        BoardModel boardModel = GetComponentInParent<BoardModel>();
        for (int x = 0; x < boardModel.board.width; x++) {
            for (int y = 0; y < boardModel.board.heigth; y++) {
                Gizmos.DrawSphere(new Vector3(GetLeft(x), GetTop(y), transform.position.z), 0.1f);
            }
        }
        
    }
}
