using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStepIndicator : MonoBehaviour {

    public float firstOffset;
    public float step;

    public void UpdatePosition(int time){
        transform.localPosition = new Vector3(time * step + firstOffset, transform.localPosition.y, transform.localPosition.z);
    }

}
