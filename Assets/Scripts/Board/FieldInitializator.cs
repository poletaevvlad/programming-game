using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoardModel))]
public class FieldInitializator : MonoBehaviour {

    private BoardModel model = null;
    public Transform componentPrefab;

    public void InitializeBoard() {
        if (model == null) {
            model = GetComponent<BoardModel>();
        }
        
        // Clearing field hierarchy
        foreach (Transform child in transform.Cast<Transform>().ToArray()) {
            if (child.name != "Board") {
                if (Application.isPlaying) {
                    Destroy(child.gameObject);
                } else {
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        // Adding components
        foreach (Component component in model.board._components) {
            AddComponent(component);
        }
    }

    private void AddComponent(Component component){
        Transform newTransform = Instantiate(componentPrefab, transform);
        newTransform.GetComponent<ComponentModel>().component = component;
        ComponentGenerator generator = newTransform.GetComponent<ComponentGenerator>();
        generator.Position();
        generator.Generate();
    }

}
