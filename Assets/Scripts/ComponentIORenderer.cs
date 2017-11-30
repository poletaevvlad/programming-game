using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentIORenderer : MonoBehaviour {

    private MeshRenderer _meshRenderer;
    private MeshRenderer meshRenderer {
        get {
            if (_meshRenderer == null) {
                _meshRenderer = GetComponent<MeshRenderer>();
            }
            return _meshRenderer;
        }
    }

    public float Radius {
        get {
            if (meshRenderer == null) {
                return 0;
            }
            return meshRenderer.material.GetFloat("_Radius");
        }

        set {
            if (Mathf.Abs(Radius - value) > Mathf.Epsilon) {
                meshRenderer.material.SetFloat("_Radius", value);
            }
        }
    }

}
