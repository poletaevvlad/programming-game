using System;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionDisplayCounterFactory : ExecutionDisplayElementFactory{

    public Text textPrefab;
    public int initialValue;

    public override RectTransform CreateElement(int index, Transform parent){
        Text text = Instantiate(textPrefab, parent);
        text.text = (index + initialValue).ToString();
        return text.GetComponent<RectTransform>();
    }

}
