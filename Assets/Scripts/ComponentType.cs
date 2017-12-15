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
                break;
            case ComponentTypeIndex.Conditional:
                return new ComponentType() {
                    width = 1,
                    height = 2,
                    inputs = new Connector[2] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                        new Connector(){x = 0, y = 1, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[2] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right },
                        new Connector(){x = 0, y = 1, direction = ConnectorDirection.Right }
                    }
                };
                break;
            case ComponentTypeIndex.LineIntersection:
                return new ComponentType() {
                    width = 1,
                    height = 1,
                    inputs = new Connector[2] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left },
                        new Connector(){x = 1, y = 0, direction = ConnectorDirection.Up }
                    },
                    outputs = new Connector[2] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right },
                        new Connector(){x = 1, y = 1, direction = ConnectorDirection.Down }
                    }
                };
                break;
            case ComponentTypeIndex.Memory:
                return new ComponentType() {
                    width = 1,
                    height = 1,
                    inputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Left }
                    },
                    outputs = new Connector[1] {
                        new Connector(){x = 0, y = 0, direction = ConnectorDirection.Right }
                    }
                };
                break;
            //Этой строчкой я просто затыкаю возмущения что не все ветве возвращают значчение
            //Просто это строчка не имеет значения, ведь теоретически 
            //там всеггда будут в эту функцую поступать только индексы, к которым есть компоненты
            default: throw new ArgumentException("Компонент с таким индексом не найден");
        }
	}
}
