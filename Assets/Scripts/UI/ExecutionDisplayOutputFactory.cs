using System;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionDisplayOutputFactory : ExecutionDisplayElementFactory {

    public Text validValueLabelPrefab;
    public Text invalidValueLabelPrefab;
    public Text expectedLabelPrefab;
    public RectTransform placeholderPrefab;
    public ExecutionManager executionManager;
    public bool isExpected;

    public override RectTransform CreateElement(int index, Transform parent) {
        bool isReal, isValid;
        float? value = executionManager.GetOutput(index, isExpected, out isReal, out isValid);
        if (value == null) {
            return Instantiate(placeholderPrefab, parent);
        } else {
            Text text = Instantiate(isReal ? (isValid ? validValueLabelPrefab : invalidValueLabelPrefab) : expectedLabelPrefab, parent);
            text.text = value.Value.ToString(); 
            return text.GetComponent<RectTransform>();
        }
    }

}
