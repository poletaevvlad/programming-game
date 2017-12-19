using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TimeChangeEvent: UnityEvent<int> { }

public class ExecutionManager : MonoBehaviour {

    public Level level;
    public TimeChangeEvent onTimeChanged;
    public BoardModel boardModel;

    public ExecutionValue executionValuePrefab;
    public Transform executionValueParent;

    public int time;

    private Dictionary<int, float> inputs = new Dictionary<int, float>();
    private Dictionary<int, float> outputs = new Dictionary<int, float>();

    public void Start() {
        foreach (Level.InputItem inputItem in level.input) {
            inputs[inputItem.time] = inputItem.value;
        }
    }

    public float? GetInput(int time) {
        if (inputs.ContainsKey(time)) {
            return inputs[time];
        } else {
            return null;
        }
    }

    private class Connection{
        public ConnectionLine connectionLine;
        public ConnectionLineRenderer connectionLineRenderer;
        public float? value = null;
        public ExecutionValue graphic;

        public Connection(ConnectionLine line, ConnectionLineRenderer renderer){
            connectionLine = line;
            connectionLineRenderer = renderer;
        }
    }

    private class InputOutput{
        public float?[] inputs;
        public float?[] outputs;
        public ComponentType type;

        public InputOutput(ComponentType componentType){
            type = componentType;
            inputs = new float?[componentType.inputs.Length];
            outputs = new float?[componentType.outputs.Length];
        }

        public void Reset(){
            for (int i = 0; i < inputs.Length; i++) {
                inputs[i] = null;
            }
            for (int i = 0; i < outputs.Length; i++) {
                outputs[i] = null;
            }
        }
    }

    private Connection[] connections;
    private Dictionary<int, InputOutput> inOut;

    [ContextMenu("Step")]
    public void ComputeStep(){
        if (connections == null) {
            connections = new Connection[boardModel.board._connections.Count];
            for (int i = 0; i < connections.Length; i++) {
                connections[i] = new Connection(boardModel.board._connections[i], 
                                                boardModel.FindLineRenderer(boardModel.board._connections[i]));
            }
            inOut = new Dictionary<int, InputOutput>(boardModel.board._components.Count);
            foreach (Component component in boardModel.board._components) {
                inOut[component.id] = new InputOutput(ComponentType.GetComponentType(component.type));
            }
        }

        foreach (InputOutput io in inOut.Values) io.Reset();

        foreach(Connection connection in connections) {
            inOut[connection.connectionLine.endComponentId].inputs[connection.connectionLine.endOutputIndex] = connection.value;
            if (connection.graphic != null) {
                connection.graphic.Disappear();
                connection.graphic = null;
            }
        }

        foreach(InputOutput io in inOut.Values) {
            io.type.compute(io.inputs, io.outputs, this);
        }

        foreach (Connection connection in connections) {
            connection.value = inOut[connection.connectionLine.startComponentId].outputs[connection.connectionLine.startOutputIndex];
            if (connection.value != null){
                connection.graphic = Instantiate(executionValuePrefab, executionValueParent);
                connection.graphic.Initialize(connection.value.Value, connection.connectionLineRenderer);
                connection.graphic.Appear();
            }
        }

        time++;
        onTimeChanged.Invoke(time);
    }

}
