using System;
using UnityEngine;

public class SummaryAndSuggestions : MonoBehaviour {
    CanvasGroup canvasGroup;

    public RectTransform parentSuggestionsTo;
    
    void Start() {
        CacheComponents();
    }

    void CacheComponents() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    public void PopulateSuggestions() {
        
    }
}