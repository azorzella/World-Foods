using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishView : MonoBehaviour, MenuListener {
    static DishView _i;
	
    /// <summary>
    /// Returns the static instance of DishView. If the instance
    /// has not been set, then it instantiates a new instance of
    /// DishView using the resource loader, caches it, and returns it
    /// </summary>
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
    public RatingMenu ratingMenu;
    
    readonly List<GameObject> entries = new();

    CanvasGroup canvasGroup;
    VerticalLayoutGroup layoutGroup;

    void Start() {
        DontDestroyOnLoad(gameObject);
        
        FindFirstObjectByType<Menu>().RegisterListener(this);
    }

    /// <summary>
    /// Caches its canvas group and the vertical layout group found on the entry parent,
    /// hides itself, and hides the rating panel
    /// </summary>
    void CacheComponents() {
        canvasGroup = GetComponent<CanvasGroup>();
        layoutGroup = parentEntriesTo.gameObject.GetComponent<VerticalLayoutGroup>();

        Close();
        HideRatingPanel();
    }
    
    /// <summary>
    /// Shows the menu displaying the passed list of dishes parented to the scroll view,
    /// resized to fit the dish entries
    /// </summary>
    /// <param name="dishes"></param>
    public void OpenDisplaying(List<Dish> dishes) {
        if (dishes.Count <= 0) {
            return;
        }
        
        ClearEntries();

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
    
    /// <summary>
    /// Clears the list of entries
    /// </summary>
    void ClearEntries() {
        while (entries.Count > 0) {
            Destroy(entries[0]);
            entries.RemoveAt(0);
        }
    }

    /// <summary>
    /// Hides itself
    /// </summary>
    public void Close() {
        SetSelfVisible(false);
    }

    bool visible;

    /// <summary>
    /// Returns whether it's currently visible
    /// </summary>
    /// <returns></returns>
    public bool IsVisible() {
        return visible;
    }
    
    /// <summary>
    /// Updates the parameters of its cached canvas group to either show or hide itself,
    /// and then automatically hides the ratings panel when it gets closed
    /// </summary>
    /// <param name="visible"></param>
    void SetSelfVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
        
        this.visible = visible;

        if (!visible) {
            HideRatingPanel();
        }
    }

    /// <summary>
    /// Shows the ratings panel for the passed dish
    /// </summary>
    /// <param name="dish"></param>
    public void ShowRatingPanelFor(Dish dish) {
        ratingMenu.SetDish(dish);
    }

    /// <summary>
    /// Hides the rating panel
    /// </summary>
    public void HideRatingPanel() {
        ratingMenu.Hide();
    }

    /// <summary>
    /// Hides itself if the user clicks another tab besides the map view
    /// </summary>
    /// <param name="currentIndex"></param>
    public void NotifyMenuStateChanged(int currentIndex) {
        if (currentIndex > 0) {
            SetSelfVisible(false);
        }
    }
}