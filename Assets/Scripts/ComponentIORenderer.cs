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

    public float radius = 0.2f;

    private void Update() {
        meshRenderer.material.SetFloat("_Radius", radius);
    }

    public void HoverStarted() {
        GetComponent<Animator>().SetBool("Hover", true);
    }

    public void HoverEnded(){
        GetComponent<Animator>().SetBool("Hover", false);
    }

}
