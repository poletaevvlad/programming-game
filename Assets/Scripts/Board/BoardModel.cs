 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class BoardModel : MonoBehaviour {
	
	public Board board;

	public UnityEvent rebuildRequiredEvent;

    public void Rebuild() {
        rebuildRequiredEvent.Invoke();
    }
}
