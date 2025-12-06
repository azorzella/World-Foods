using System;
using System.Collections.Generic;
using UnityEngine;

public interface RatingMenuListener {
    void NotifyRatingChanged(int newRating);
}

public class RatingMenu : MonoBehaviour {
    Dish currentDish;

    readonly List<RatingMenuListener> listeners = new();

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
}