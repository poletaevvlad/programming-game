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

    public UnityEvent onProgramStarted;
    public UnityEvent onprogramTerminated;
    public UnityEvent onAnimationStarted;
    public UnityEvent onAnimationEnded;
    public UnityEvent onRunnignStarted;
    public UnityEvent onRunningEnded;

    public int time;
    public bool isRunning { get; private set; }
    private Dictionary<int, float> inputs = new Dictionary<int, float>();

    public float animationTime;

    public class OutputData {
        public float provided;
        public float? expected;
    }
    private Dictionary<int, OutputData> outputs = new Dictionary<int, OutputData>();

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

    public float? GetOutput(int time, bool expected, out bool isReal, out bool isValid){
        isReal = true;
        isValid = false;
        if (time < this.time) {
            if (outputs.ContainsKey(time)) {
                isValid = outputs[time].expected != null && Mathf.Abs(outputs[time].expected.Value - outputs[time].provided) < 1e-5;
                return expected ? outputs[time].expected : outputs[time].provided;
            }
        } else if (expected) {
            isReal = false;
            int index = time - this.time + outputs.Count;
            if (index < level.output.Length) {
                return level.output[index];
            }
        }
        return null;
    }

    public void RegisterOutput(float value){
        OutputData od = new OutputData();
        od.provided = value;
        od.expected = outputs.Count >= level.output.Length ? (float?)null : level.output[outputs.Count];
        outputs[time] = od;
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
        public Component component;

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

    public void ComputeStep(){
        isRunning = true;
        if (connections == null) {
            connections = new Connection[boardModel.board._connections.Count];
            for (int i = 0; i < connections.Length; i++) {
                connections[i] = new Connection(boardModel.board._connections[i], 
                                                boardModel.FindLineRenderer(boardModel.board._connections[i]));
            }
            inOut = new Dictionary<int, InputOutput>(boardModel.board._components.Count);
            foreach (Component component in boardModel.board._components) {
                inOut[component.id] = new InputOutput(ComponentType.GetComponentType(component.type));
                inOut[component.id].component = component;
                inOut[component.id].component.parameterOpt = null;
            }
            onProgramStarted.Invoke();
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
            io.type.compute(io.inputs, io.outputs, this, ref io.component.parameterOpt, io.component.parmater);
        }

        bool anyAnimation = false;
        foreach (Connection connection in connections) {
            connection.value = inOut[connection.connectionLine.startComponentId].outputs[connection.connectionLine.startOutputIndex];
            if (connection.value != null){
                connection.graphic = Instantiate(executionValuePrefab, executionValueParent);
                connection.graphic.Initialize(connection.value.Value, connection.connectionLineRenderer);
                connection.graphic.Appear();
                anyAnimation = true;
            }
        }
        if (anyAnimation) {
            animationLeftTime = animationTime;
            waitingAnimationOver = true;
            onAnimationStarted.Invoke();
        }

        time++;
        onTimeChanged.Invoke(time);
    }

    public void Break() {
        time = 0;
        foreach(Connection connection in connections) {
            if (connection.graphic != null) {
                connection.graphic.Disappear();
            }
        }
        connections = null;
        inOut = null;
        onTimeChanged.Invoke(time);
        onprogramTerminated.Invoke();
        outputs.Clear();
        isRunning = false;
        if (runningAutomatic) {
            onRunningEnded.Invoke();
            runningAutomatic = false;
        }
    }

    private bool waitingAnimationOver;
    private float animationLeftTime;

    private bool runningAutomatic;
    private float automaticLeftTime;
    public float automaticRunningTime;

    public void Update(){
        if (waitingAnimationOver) {
            animationLeftTime -= Time.deltaTime;
            if (animationLeftTime < 0) {
                waitingAnimationOver = false;
                onAnimationEnded.Invoke();
            }
        }
        if (runningAutomatic) {
            automaticLeftTime -= Time.deltaTime;
            if (automaticLeftTime < 0) {
                automaticLeftTime = automaticRunningTime;
                ComputeStep();
            }
        }
    }

    public void RunAutomatic() {
        automaticLeftTime = automaticRunningTime;
        runningAutomatic = true;
        onRunnignStarted.Invoke();
        ComputeStep();
    }

    public void PauseAutomatic() {
        if (runningAutomatic) { 
            runningAutomatic = false;
            onRunningEnded.Invoke();
        }
    }

}
