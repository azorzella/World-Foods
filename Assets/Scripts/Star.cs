using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Star : MonoBehaviour, RatingMenuListener, IPointerClickHandler {
    int index;
    Image image;

    RatingMenu ratingMenu;
    
    void Awake() {
        Initialize();
    }

    /// <summary>
    /// Caches its sibling index and image component. Then it
    /// caches the first instance of RatingMenu it can find and
    /// registers itself to listen to it
    /// </summary>
    void Initialize() {
        index = transform.GetSiblingIndex();
        image = GetComponent<Image>();
        
        ratingMenu = FindFirstObjectByType<RatingMenu>();
        ratingMenu.RegisterListener(this);
    }

    readonly Color filledColor = new Color(0.7F, 0.5F, 0.0F);
    readonly Color emptyColor = Color.white;
    
    /// <summary>
    /// Colors itself according to whether its index is less than or
    /// equal to the new rating value
    /// </summary>
    /// <param name="newRating"></param>
    public void NotifyRatingChanged(int newRating) {
        image.color = index + 1 <= newRating ? filledColor : emptyColor;
    }

    /// <summary>
    /// Sets the rating of the current dish in the rating menu to its
    /// index when clicked. This is a circular dependency
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        ratingMenu.SetRating(index + 1);
    }
}