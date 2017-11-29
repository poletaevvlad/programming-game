using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IOParameters", menuName = "IO Builder parameters")]
public class IOBuilderParameters : ScriptableObject {
    [Header("Circle relative position")]
    public Vector3 leftCirclePosition;
    public Vector3 topCirclePosition;
    public Vector3 rightCirclePosition;
    public Vector3 bottomCirclePosition;

    [Header("Left arrow")]
    public Vector3 leftArrowPosition;

    [Range(0, 360)]
    public float leftArrowRotation;

    [Header("Top arrow")]
    public Vector3 topArrowPosition;

    [Range(0, 360)]
    public float topArrowRotation;

    [Header("Right arrow")]
    public Vector3 rightArrowPosition;

    [Range(0, 360)]
    public float rightArrowRotation;

    [Header("Bottom arrow")]
    public Vector3 bottomArrowPosition;

    [Range(0, 360)]
    public float bottomArrowRotation;

    [Header("Prefabs")]
    public Transform arrowPrefab;
    public Transform circlePrefab;

}
