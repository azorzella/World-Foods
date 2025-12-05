using System;
using System.Collections.Generic;
using Unity.VisualScripting.TextureAssets;
using UnityEngine;
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

    readonly List<GameObject> entries = new();

    CanvasGroup canvasGroup;
    VerticalLayoutGroup layoutGroup;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    void CacheComponents() {
        canvasGroup = GetComponent<CanvasGroup>();
        layoutGroup = parentEntriesTo.gameObject.GetComponent<VerticalLayoutGroup>();
        
        SetVisible(false);
    }
    
    public void OpenDisplaying(List<Dish> dishes) {
        Clear();

        GameObject dishEntry = ResourceLoader.LoadObject("DishEntry");

        GameObject newEntry = null;
        
        foreach (var dish in dishes) {
            newEntry = Instantiate(dishEntry, parentEntriesTo);
            newEntry.GetComponent<DishEntry>().Initialize(dish);
            entries.Add(newEntry);
        }

        int dishCount = dishes.Count;

        if (dishCount > 0) {
            Vector2 newSize = new Vector2(
                parentEntriesTo.sizeDelta.x,
                newEntry.GetComponent<RectTransform>().sizeDelta.y * dishCount + layoutGroup.spacing * dishCount);
            parentEntriesTo.sizeDelta = newSize;    
        }
        
        SetVisible(true);
    }
    
    void Clear() {
        while (entries.Count > 0) {
            Destroy(entries[0]);
            entries.RemoveAt(0);
        }
    }

    public void Close() {
        SetVisible(false);
    }

    bool visible;

    public bool IsVisible() {
        return visible;
    }
    
    void SetVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
        
        this.visible = visible;
    }
}