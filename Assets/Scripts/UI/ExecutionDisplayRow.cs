using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionDisplayRow : MonoBehaviour {
    private ExecutionDisplayElementFactory factory;

    void Start() {
        factory = GetComponent<ExecutionDisplayElementFactory>();
        if (factory == null) {
            Debug.LogError("ExecutionDisplayElementFactory subclass is not found among execution display row's components.");
            return;
        }
        Debug.Log(factory.GetType());
    }
}
