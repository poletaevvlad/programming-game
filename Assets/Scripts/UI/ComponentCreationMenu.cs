using UnityEngine;
using UnityEngine.UI;

public class ComponentCreationMenu: MonoBehaviour{
    public ComponentTypeIndex [] types;

    public Button buttonPrefab;
    public RectTransform container;

    public void Start() {
        foreach (ComponentTypeIndex type in types){
            Button newButton = Instantiate(buttonPrefab, container);
            newButton.GetComponentInChildren<Text>().text = ComponentType.GetComponentType(type).label;
        }
    }

    public void Show(){
        gameObject.SetActive(true);
    }

}
