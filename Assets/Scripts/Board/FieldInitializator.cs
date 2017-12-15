using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoardModel))]
public class FieldInitializator : MonoBehaviour {

    private BoardModel model = null;
    public Transform componentPrefab;

    public void InitializeBoard() {
        if (model == null) {
            model = GetComponent<BoardModel>();
        }

        // TODO: Use components from the model when implemented
        AddComponent(new Component() {
            type = ComponentTypeIndex.Addition,
            coord = new Coord() { x = 5, y = 3 }
        });

        AddComponent(new Component() {
            type = ComponentTypeIndex.Addition,
            coord = new Coord() { x = 9, y = 5 }
        });
    }

    private void AddComponent(Component component){
        Transform newTransform = Instantiate(componentPrefab, transform);
        newTransform.GetComponent<ComponentModel>().component = component;
        ComponentGenerator generator = newTransform.GetComponent<ComponentGenerator>();
        generator.Position();
        generator.Generate();
    }

}
