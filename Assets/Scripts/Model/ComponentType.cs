using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentType {

    public delegate void ComponentComputationDelegate(float?[] input, float?[] output, ExecutionManager executionManager);

	public int width { get; set;}
	public int height { get; set;}
	public string label { get; set;}
	public Connector[] inputs { get; set;}
	public Connector[] outputs { get; set;}

    public ComponentComputationDelegate compute;

	public static ComponentType GetComponentType (ComponentTypeIndex index){
        switch (index) {
            //break везде я так понимаю лишний, ибо до него никогда не дойдет, ретурн выводить всегда будет с функции
            case ComponentTypeIndex.Addition:
                return new ComponentType() {
                    label = "add",
                    width = 1,
                    height = 1,
                    inputs = new Connector[3] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Up },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Down }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate(float?[] input, float?[] output, ExecutionManager executionManager) {
                        float sum = 0;
                        bool hasInputs = false;
                        for (int i = 0; i < 3; i++) {
                            if (input[i] != null) {
                                sum += input[i].Value;
                                hasInputs = true;
                            }
                        }
                        if (hasInputs) {
                            output[0] = sum;
                        }
                    }
                };
            case ComponentTypeIndex.Negative:
                return new ComponentType() {
                    label = "neg",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate(float?[] input, float?[] output, ExecutionManager executionManager) {
                        if (input[0] != null) output[0] = -input[0].Value;
                    }
                };
            case ComponentTypeIndex.Multiplication:
                return new ComponentType() {
                    label = "mul",
                    width = 1,
                    height = 1,
                    inputs = new Connector[3] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Up },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Down }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {
                        float product = 1;
                        bool hasInputs = false;
                        for (int i = 0; i < 3; i++) {
                            if (input[i] != null) {
                                product *= input[i].Value;
                                hasInputs = true;
                            }
                        }
                        if (hasInputs) {
                            output[0] = product;
                        }
                    }
                };
            case ComponentTypeIndex.Inverse:
                return new ComponentType() {
                    label = "1/x",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {
                        if (input[0] != null) output[0] = 1 / input[0].Value;
                    }
                };
            case ComponentTypeIndex.Memory:
                return new ComponentType() {
                    label = "mem",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {}
                };
            case ComponentTypeIndex.Conditional:
                return new ComponentType() {
                    label = "if",
                    width = 1,
                    height = 2,
                    inputs = new Connector[3] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Up },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                        new Connector(){x = 0, y = 1, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[2] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right },
                        new Connector(){x = 0, y = 1, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {
                        if (input[0] != null) {
                            if (Mathf.Abs(input[0].Value) > 1e-5) {
                                output[0] = input[1];
                            } else {
                                output[1] = input[2];
                            }
                        }
                    }
                };
            case ComponentTypeIndex.Value:
                return new ComponentType() {
                    label = "N",
                    width = 1,
                    height = 1,
                    inputs = new Connector[0] { },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) { }
                };
            case ComponentTypeIndex.Increment:
                return new ComponentType() {
                    label = "inc",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {
                        if (input[0] != null) {
                            output[0] = input[0].Value + 1;
                        }
                    }
                };
            case ComponentTypeIndex.Decrement:
                return new ComponentType() {
                    label = "Dec",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {
                        if (input[0] != null) {
                            output[0] = input[0].Value - 1;
                        }
                    }
                };
            case ComponentTypeIndex.Input:
                return new ComponentType() {
                    label = "in",
                    width = 1,
                    height = 1,
                    inputs = new Connector[0],
                    outputs = new Connector[] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    },
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {
                        output[0] = executionManager.GetInput(executionManager.time);
                    }
                };
            case ComponentTypeIndex.Output:
                return new ComponentType() {
                    label = "out",
                    width = 1,
                    height = 1,
                    inputs = new Connector[] {
                        new Connector(){ x = 0, y = 0, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[0],
                    compute = delegate (float?[] input, float?[] output, ExecutionManager executionManager) {
                        if (input[0] != null) {
                            executionManager.RegisterOutput(input[0].Value);
                        }
                    }
                };
            //Этой строчкой я просто затыкаю возмущения что не все ветве возвращают значчение
            //Просто это строчка не имеет значения, ведь теоретически 
            //там всеггда будут в эту функцую поступать только индексы, к которым есть компоненты
            default: throw new ArgumentException("Компонент с таким индексом не найден");
        }
	}
}
