using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
class ExecutionValue : MonoBehaviour{

    public ConnectionLineRenderer lineRenderer;
    public float position;
    public Text text;

    private Camera mainCamera;

    private void Start(){
        mainCamera = GameObject.Find("/MainCamera").GetComponent<Camera>();
    }

    private void Update() {
        float pos = position;
        int i = 0;
        while (i < lineRenderer.coordinates.Count - 1) {
            float dist = ConnectionLineRenderer.ManhattanDistance(lineRenderer.coordinates[i], lineRenderer.coordinates[i + 1]);
            if (pos < dist) {
                Vector3 position = Vector3.Lerp(lineRenderer.CoordToVec3(lineRenderer.coordinates[i]), 
                                                lineRenderer.CoordToVec3(lineRenderer.coordinates[i + 1]), pos);
                position = lineRenderer.transform.localToWorldMatrix * position;
                transform.position = mainCamera.WorldToScreenPoint(position);
                return;
            } else {
                pos -= dist;
            }
            i++;
        }
    }

    public void Appear(){
        GetComponent<Animator>().SetTrigger("StartRequested");
    }

    public void Disappear(){
        GetComponent<Animator>().SetTrigger("RemoveRequested");
    }

    public void Initialize(float value, ConnectionLineRenderer lineRenderer) {
        this.lineRenderer = lineRenderer;
        text.text = value.ToString();
    }

    public void AnimationEnded(){
        Debug.Log("Should destroy");
    }

}
