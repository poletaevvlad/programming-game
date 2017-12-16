using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutionDisplayElementFactory : MonoBehaviour {

    public abstract RectTransform CreateElement(int index, Transform parent);

}
