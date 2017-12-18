using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class Level : ScriptableObject {

    [Serializable]
    public struct InputItem {
        public int time;
        public float value;
    }

    public string title;
    public string description;
    public InputItem[] input;
    public float[] output;

}
