using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConnectionLine {
    public int startComponentId;
    public int startOutputIndex;
    public int endComponentId;
    public int endOutputIndex;
    public Coord[] intermediatePoints;
}
