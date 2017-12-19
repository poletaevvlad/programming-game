using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TimeChangeEvent: UnityEvent<int> { }

public class ExecutionManager : MonoBehaviour {

    public Level level;
    public TimeChangeEvent onTimeChanged;

    public int time;

    private Dictionary<int, float> inputs = new Dictionary<int, float>();
    private Dictionary<int, float> outputs = new Dictionary<int, float>();

    public void Start(){
        foreach (Level.InputItem inputItem in level.input) {
            inputs[inputItem.time] = inputItem.value;
        }
    }

    public float? GetInput(int time){
        if (inputs.ContainsKey(time)) {
            return inputs[time];
        } else {
            return null;
        }
    }

    [ContextMenu("Step")]
    public void ComputeSpep(){
        time++;
        onTimeChanged.Invoke(time);
    }

}
