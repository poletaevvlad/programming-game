using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public class CreationEvent : UnityEvent<ComponentTypeIndex> { }

public class ComponentCreationMenu: MonoBehaviour{
    
    public ComponentTypeIndex [] types;

    public Button buttonPrefab;
    public RectTransform container;

    public CreationEvent onComponentCreated;

    public void Start(){
        foreach (ComponentTypeIndex type in types) {
            Button newButton = Instantiate(buttonPrefab, container);
            newButton.GetComponentInChildren<Text>().text = ComponentType.GetComponentType(type).label;
            newButton.onClick.AddListener(delegate (){
                onComponentCreated.Invoke(type);
            });
        }
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }

}
