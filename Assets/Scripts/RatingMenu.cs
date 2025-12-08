using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RatingMenu : MonoBehaviour {
    Dish currentDish;

    TextMeshProUGUI text;
    CanvasGroup canvasGroup;

    readonly List<RatingMenuListener> listeners = new();

    void Awake() {
        CacheComponents();
    }

    /// <summary>
    /// Caches the text component found in its children and
    /// its canvas group
    /// </summary>
    void CacheComponents() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    /// <summary>
    /// Sets the current dish being rated and notifies the rating
    /// listeners as the current dish was updated
    /// </summary>
    /// <param name="dish"></param>
    public void SetDish(Dish dish) {
        currentDish = dish;
        text.text = $"Rate {dish.GetName()}";
        
        NotifyListeners();
        Show();
    }
    
    /// <summary>
    /// Adds the passed listener to the list of listeners
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(RatingMenuListener listener) {
        listeners.Add(listener);

        if (currentDish != null) {
            listener.NotifyRatingChanged(currentDish.GetRating());
        }
    }

    /// <summary>
    /// Notifies all the rating listeners of the current dish's rating
    /// </summary>
    void NotifyListeners() {
        foreach (var listener in listeners) {
            listener.NotifyRatingChanged(currentDish.GetRating());
        }
    }

    /// <summary>
    /// Sets the current dish's rating
    /// </summary>
    /// <param name="newRating"></param>
    public void SetRating(int newRating) {
        currentDish.SetRating(newRating);
        
        NotifyListeners();
    }

    /// <summary>
    /// Sets the alpha, interactability, and raycast blocking flag of
    /// the cached canvas group according to whether it's visible or
    /// not
    /// </summary>
    /// <param name="visible"></param>
    void SetCanvasGroupVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
    
    /// <summary>
    /// Opens the menu
    /// </summary>
    void Show() {
        SetCanvasGroupVisible(true);
    }

    /// <summary>
    /// Closes the menu
    /// </summary>
    public void Hide() {
        SetCanvasGroupVisible(false);
    }
}