using System;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionDisplayInputFactory : ExecutionDisplayElementFactory {

    public Text valueLabelPrefab;
    public RectTransform placeholderPrefab;
    public ExecutionManager executionManager;

    public override RectTransform CreateElement(int index, Transform parent){
        float? value = executionManager.GetInput(index);
        if (value != null) {
            Text text = Instantiate(valueLabelPrefab, parent);
            text.text = value.Value.ToString();
            return text.GetComponent<RectTransform>();
        } else {
            return Instantiate(placeholderPrefab, parent);
        }
    }
}
