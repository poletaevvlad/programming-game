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

    public Vector3 GetCirclePosition(ConnectorDirection direction){
        switch (direction) {
            case ConnectorDirection.Left:
                return leftCirclePosition;
            case ConnectorDirection.Right:
                return rightCirclePosition;
            case ConnectorDirection.Up:
                return topCirclePosition;
            case ConnectorDirection.Down:
                return bottomCirclePosition;
            default:
                return Vector3.zero;
        }
    }

    public Vector3 GetArrowPosition(ConnectorDirection direction){
        switch (direction) {
            case ConnectorDirection.Left:
                return leftArrowPosition;
            case ConnectorDirection.Right:
                return rightArrowPosition;
            case ConnectorDirection.Up:
                return topArrowPosition;
            case ConnectorDirection.Down:
                return bottomArrowPosition;
            default:
                return Vector3.zero;
        }
    }

    public float GetArrowRotation(ConnectorDirection direction){
        switch (direction) {
            case ConnectorDirection.Left:
                return leftArrowRotation;
            case ConnectorDirection.Right:
                return rightArrowRotation;
            case ConnectorDirection.Up:
                return topArrowRotation;
            case ConnectorDirection.Down:
                return bottomArrowRotation;
            default:
                return 0.0f;
        }
    }

}
