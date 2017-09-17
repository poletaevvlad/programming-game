using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentType {

	int width { get; set;}
	int heigth { get; set;}
	string lebel { get; set;}
	Connector[] inputs { get; set;}
	Connector[] outputs { get; set;}

	public ComponentType getComponentType (ComponentTypeIndex index){
		return new ComponentType();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
