using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class BoardModel : MonoBehaviour {

	public Board _board;
	public Board board {
		private set{ _board = value; }
		get{ return _board; }
	}

	public UnityEvent rebuildRequiredEvent;

	public void GenerateRandom(){
		board = new Board ();
		board.width = 16;
		board.heigth = 10;

		rebuildRequiredEvent.Invoke ();
	}

}
