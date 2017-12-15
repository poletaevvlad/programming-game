using UnityEngine;

public class ComponentModel : MonoBehaviour {

    // TODO: For testing purposes. Remove when model creation is implemented.
    public Component component = new Component() {
        coord = new Coord() { x = 2, y = 3 },
        type = ComponentTypeIndex.Addition
    };

}
