using System;
using System.Collections.Generic;
using Unity.VisualScripting.TextureAssets;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DishView : MonoBehaviour {
    static DishView _i;
	
    public static DishView i {
        get {
            if (_i == null) {
                DishView obj = Resources.Load<DishView>("DishView");

                _i = Instantiate(obj);
                _i.CacheComponents();
            }
            return _i;
        }
    }
    
    public RectTransform parentEntriesTo;
    [FormerlySerializedAs("ratingPanelCanvasGroup")] public RatingMenu ratingMenu;
    
    readonly List<GameObject> entries = new();

    CanvasGroup canvasGroup;
    VerticalLayoutGroup layoutGroup;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    void CacheComponents() {
        canvasGroup = GetComponent<CanvasGroup>();
        layoutGroup = parentEntriesTo.gameObject.GetComponent<VerticalLayoutGroup>();
        
        SetSelfVisible(false);
        HideRatingPanel();
    }
    
    public void OpenDisplaying(List<Dish> dishes) {
        Clear();

        GameObject dishEntry = ResourceLoader.LoadObject("DishEntry");

        GameObject newEntry = null;
        
        foreach (var dish in dishes) {
            newEntry = Instantiate(dishEntry, parentEntriesTo);
            newEntry.GetComponent<DishEntry>().Initialize(this, dish);
            entries.Add(newEntry);
        }

        int dishCount = dishes.Count;

        if (dishCount > 0) {
            Vector2 newSize = new Vector2(
                parentEntriesTo.sizeDelta.x,
                newEntry.GetComponent<RectTransform>().sizeDelta.y * dishCount + layoutGroup.spacing * dishCount);
            parentEntriesTo.sizeDelta = newSize;    
        }
        
        SetSelfVisible(true);
    }
    
    void Clear() {
        while (entries.Count > 0) {
            Destroy(entries[0]);
            entries.RemoveAt(0);
        }
    }

    public void Close() {
        SetSelfVisible(false);
    }

    bool visible;

    public bool IsVisible() {
        return visible;
    }
    
    void SetCanvasGroupVisible(CanvasGroup group, bool visible) {
        group.alpha = visible ? 1 : 0;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }

    void SetSelfVisible(bool visible) {
        SetCanvasGroupVisible(canvasGroup, visible);
        this.visible = visible;

        if (!visible) {
            HideRatingPanel();
        }
    }

    public void ShowRatingPanelFor(Dish dish) {
        ratingMenu.SetDish(dish);
    }

    public void HideRatingPanel() {
        ratingMenu.Hide();
    }
}