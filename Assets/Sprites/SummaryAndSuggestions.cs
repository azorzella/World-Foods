using System;
using TMPro;
using UnityEngine;

public class SummaryAndSuggestions : MonoBehaviour {
    public TextMeshProUGUI countryCountText;
    public TextMeshProUGUI dishCountText;
    public RectTransform parentSuggestionsTo;
    
    CanvasGroup canvasGroup;

    void Start() {
        CacheComponents();
    }

    void CacheComponents() {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void SetVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    public void UpdateSummary(UserData userData) {
        countryCountText.text = $"{userData.NumUniqueCountriesEatenFrom()} Countries";
        dishCountText.text = $"{userData.NumUniqueDishesEaten()} Dishes";
    }

    FoodSuggestionEntry InstantiateFoodSuggestionEntry() {
        GameObject newEntry = ResourceLoader.InstantiateObject("FoodSuggestionEntry", Vector2.zero, Quaternion.identity);
        newEntry.GetComponent<RectTransform>().SetParent(parentSuggestionsTo);

        FoodSuggestionEntry result = newEntry.GetComponent<FoodSuggestionEntry>();

        return result;
    }
    
    public void PopulateSuggestions(UserData userData) {
        InstantiateFoodSuggestionEntry().PopulatePersonalFavorite(userData);        
        InstantiateFoodSuggestionEntry().PopulateFamiliarSuggestion(userData);        
        InstantiateFoodSuggestionEntry().PopulateUnfamiliarSuggestion(userData);        
        
        foreach (var friend in userData.GetFriends()) {
            InstantiateFoodSuggestionEntry().PopulateFriendSuggestion(friend);
        }
    }
}