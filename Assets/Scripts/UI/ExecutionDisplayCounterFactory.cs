using System;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionDisplayCounterFactory : ExecutionDisplayElementFactory{

    public Text textPrefab;

    public override RectTransform CreateElement(int index, Transform parent){
        Text text = Instantiate(textPrefab, parent);
        text.text = index.ToString();
        return text.GetComponent<RectTransform>();

    }

}
