using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentType {

	public int width { get; set;}
	public int height { get; set;}
	public string label { get; set;}
	public Connector[] inputs { get; set;}
	public Connector[] outputs { get; set;}

	public static ComponentType GetComponentType (ComponentTypeIndex index){
        switch (index) {
            //break везде я так понимаю лишний, ибо до него никогда не дойдет, ретурн выводить всегда будет с функции
            case ComponentTypeIndex.Addition:
                return new ComponentType() {
                    label = "+",
                    width = 1,
                    height = 1,
                    inputs = new Connector[3] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Up },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Down }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    }
                };
            case ComponentTypeIndex.Negative:
                return new ComponentType() {
                    label = "-",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    }
                };
            case ComponentTypeIndex.Multiplication:
                return new ComponentType() {
                    label = "x",
                    width = 1,
                    height = 1,
                    inputs = new Connector[3] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Up },
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Down }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
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
                    }
                };
            case ComponentTypeIndex.Memory:
                return new ComponentType() {
                    label = "MEM",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    }
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
                    }
                };
            case ComponentTypeIndex.Increment:
                return new ComponentType() {
                    label = "Inc",
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
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
                    outputs = new Connector[0]
                };
            //Этой строчкой я просто затыкаю возмущения что не все ветве возвращают значчение
            //Просто это строчка не имеет значения, ведь теоретически 
            //там всеггда будут в эту функцую поступать только индексы, к которым есть компоненты
            default: throw new ArgumentException("Компонент с таким индексом не найден");
        }
	}
}
