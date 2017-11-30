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
            return meshRenderer.material.GetFloat("_Radius");
        }

        set {
            meshRenderer.material.SetFloat("_Radius", value);
        }
    }

}
