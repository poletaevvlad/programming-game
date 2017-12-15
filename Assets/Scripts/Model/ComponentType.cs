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
        return new ComponentType() {
            width = 1,
            height = 1,
            inputs = new Connector[1] {
                new Connector() { x = 0, y = 0, direction = ConnectorDirection.Left }
            },
            outputs = new Connector[1] {
                new Connector() { x = 0, y = 0, direction = ConnectorDirection.Right }  
            }
        };
	}
}
