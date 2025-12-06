using TMPro;
using UnityEngine;

public class DishEntry : MonoBehaviour {
    TextMeshProUGUI textComponent;
    Dish dishCache;
    
    public void Initialize(Dish dish) {
        dishCache = dish;
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = dishCache.GetName();
    }
}