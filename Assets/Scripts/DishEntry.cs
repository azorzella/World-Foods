using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DishEntry : MonoBehaviour, IPointerClickHandler {
    TextMeshProUGUI textComponent;
    Dish dishCache;

    DishView dishView;
    
    public void Initialize(DishView dishView, Dish dish) {
        this.dishView = dishView;
        dishCache = dish;
        
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = dishCache.GetName();
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        dishView.ShowRatingPanelFor(dishCache);
    }

    public void HideRatingPanel_BucketBrigade() {
        dishView.HideRatingPanel();
    }
}