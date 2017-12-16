using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollAutoExtension : MonoBehaviour {

    private ScrollRect scrollRect;
    private RectTransform rectTransform;
    public RectTransform viewportTransform;
    public float addedSize;

	void Start () {
        scrollRect = GetComponent<ScrollRect>();
        rectTransform = GetComponent<RectTransform>();

        OnScrolled();
	}

    public void OnScrolled(){
        viewportTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 
                rectTransform.rect.width - viewportTransform.localPosition.x + addedSize);
    }
	
	
}
