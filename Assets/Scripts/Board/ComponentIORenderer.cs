using System;
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

    public bool isInput;
    public Connector connector;
    public float radius = 0.2f;

    private void Update() {
        meshRenderer.material.SetFloat("_Radius", radius);
    }

    public void HoverStarted() {
        if (!isInput) {
            GetComponent<Animator>().SetBool("Hover", true);
        }
    }

    public void HoverEnded(){
        if (!isInput) {
            GetComponent<Animator>().SetBool("Hover", false);   
        }
    }

    public void Pressed() {
        if (!isInput) {
            GetComponent<Animator>().SetBool("Pressed", true);
        }
    }

    public void Released() {
        if (!isInput) {
            GetComponent<Animator>().SetBool("Pressed", false);
        }
    }
}
