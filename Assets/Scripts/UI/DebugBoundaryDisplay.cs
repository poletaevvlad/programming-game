using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBoundaryDisplay : MonoBehaviour {

    private void OnDrawGizmos(){
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 topLeft = rt.position + new Vector3(rt.rect.xMin, rt.rect.yMin);
        Vector3 topRight = rt.position + new Vector3(rt.rect.xMax, rt.rect.yMin);
        Vector3 bottomLeft = rt.position + new Vector3(rt.rect.xMin, rt.rect.yMax);
        Vector3 bottomRight = rt.position + new Vector3(rt.rect.xMax, rt.rect.yMax);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(rt.transform.localToWorldMatrix * topLeft, rt.transform.localToWorldMatrix * topRight);
        Gizmos.DrawLine(rt.transform.localToWorldMatrix * topLeft, rt.transform.localToWorldMatrix * bottomLeft);
        Gizmos.DrawLine(rt.transform.localToWorldMatrix * bottomRight, rt.transform.localToWorldMatrix * bottomLeft);
        Gizmos.DrawLine(rt.transform.localToWorldMatrix * bottomRight, rt.transform.localToWorldMatrix * topRight);
    }

}
