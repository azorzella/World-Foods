using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands.CheckIn.Progress;
using TMPro;
using UnityEngine;

public class RatingMenu : MonoBehaviour {
    Dish currentDish;

    TextMeshProUGUI text;
    CanvasGroup canvasGroup;

    readonly List<RatingMenuListener> listeners = new();

    void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void SetCanvasGroupVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
    
    public void SetDish(Dish dish) {
        currentDish = dish;
        text.text = $"Rate {dish.GetName()}";
        
        NotifyListeners();
        Show();
    }
    
    public void RegisterListener(RatingMenuListener listener) {
        listeners.Add(listener);

        if (currentDish != null) {
            listener.NotifyRatingChanged(currentDish.GetRating());
        }
    }

    void NotifyListeners() {
        foreach (var listener in listeners) {
            listener.NotifyRatingChanged(currentDish.GetRating());
        }
    }

    public void SetRating(int newRating) {
        currentDish.SetRating(newRating);
        
        NotifyListeners();
    }

    void Show() {
        SetCanvasGroupVisible(true);
    }

    public void Hide() {
        SetCanvasGroupVisible(false);
    }
}