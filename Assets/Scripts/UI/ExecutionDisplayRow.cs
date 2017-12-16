using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class ExecutionDisplayRow : MonoBehaviour {
    private ExecutionDisplayElementFactory factory;

    public ScrollRect scrollRect;
    public RectTransform viewportRect;

    public float firstOffset = 15;
    public float step = 30;
    public float yOffset = -8f;
    
    private int firstIndex = -1;
    private int lastIndex = -1;

    LinkedList<RectTransform> items = new LinkedList<RectTransform>();

    private UnityAction<Vector2> scrollEvent;

    void Start() {
        scrollEvent = (Vector2 v2) => OnScrolled();
        factory = GetComponent<ExecutionDisplayElementFactory>();
        if (factory == null) {
            Debug.LogError("ExecutionDisplayElementFactory subclass is not found among execution display row's components.");
            return;
        }
        scrollRect.onValueChanged.AddListener(scrollEvent);
        OnScrolled();
    }

    private void OnDestroy(){
        scrollRect.onValueChanged.RemoveListener(scrollEvent);
    }

    public void OnScrolled() {
        float width = scrollRect.GetComponent<RectTransform>().rect.width;
        float startOffset = -viewportRect.localPosition.x;
        float endOffset = startOffset + width;

        // Removing items on the right
        while (items.Count > 0 && items.Last.Value.localPosition.x + step > endOffset) {
            Destroy(items.Last.Value.gameObject);
            items.RemoveLast();
            lastIndex--;
        }

        // Appending items on the right
        while (items.Count == 0 || items.Last.Value.localPosition.x + step < endOffset) {
            lastIndex++;
            if (items.Count == 0) {
                firstIndex = 0;
            }
            RectTransform newObject = factory.CreateElement(lastIndex, transform);
            newObject.localPosition = new Vector3(firstOffset + step * lastIndex, yOffset);
            items.AddLast(newObject);
        }
        
        // Removing items on the left
        while (items.Count > 0 && items.First.Value.localPosition.x < startOffset) {
            Destroy(items.First.Value.gameObject);
            items.RemoveFirst();
            firstIndex++;
        }

        // Appending items on the left
        while (items.Count == 0 || (firstIndex > 0 && items.First.Value.localPosition.x > startOffset)) {
            firstIndex--;
            RectTransform newObject = factory.CreateElement(firstIndex, transform);
            newObject.localPosition = new Vector3(firstOffset + step * firstIndex, yOffset);
            items.AddFirst(newObject);
            if (firstIndex < -50) return;
        }

    }
}