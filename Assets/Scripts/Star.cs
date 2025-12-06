using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Star : MonoBehaviour, RatingMenuListener, IPointerClickHandler {
    int index;
    Image image;

    RatingMenu ratingMenu;
    
    void Awake() {
        index = transform.GetSiblingIndex();
        image = GetComponent<Image>();
        
        ratingMenu = FindFirstObjectByType<RatingMenu>();
        ratingMenu.RegisterListener(this);
    }

    readonly Color filledColor = new Color(0.7F, 0.5F, 0.0F);
    readonly Color emptyColor = Color.white;
    
    public void NotifyRatingChanged(int newRating) {
        image.color = newRating >= index + 1 ? filledColor : emptyColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        ratingMenu.SetRating(index + 1);
    }
}