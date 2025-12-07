using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummaryAndSuggestions : MonoBehaviour {
    public TextMeshProUGUI countryCountText;
    public TextMeshProUGUI dishCountText;
    public RectTransform parentSuggestionsTo;
    
    CanvasGroup canvasGroup;

    readonly List<GameObject> suggestionEntries = new();
    
    VerticalLayoutGroup verticalLayoutGroup;
    
    void Start() {
        CacheComponents();
    }

    void CacheComponents() {
        canvasGroup = GetComponent<CanvasGroup>();
        verticalLayoutGroup = parentSuggestionsTo.GetComponent<VerticalLayoutGroup>();
        
        SetVisible(false);
    }

    public void Show(UserData user) {
        UpdateSummary(user);
        PopulateSuggestions(user);
        
        SetVisible(true);
    }

    public void Hide() {
        SetVisible(false);
    }
    
    void SetVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    void UpdateSummary(UserData userData) {
        countryCountText.text = $"{userData.NumUniqueCountriesEatenFrom()} Countries";
        dishCountText.text = $"{userData.NumUniqueDishesEaten()} Dishes";
    }

    void ClearEntries() {
        while (suggestionEntries.Count > 0) {
            Destroy(suggestionEntries[0]);
            suggestionEntries.RemoveAt(0);
        }
    }
    
    FoodSuggestionEntry InstantiateFoodSuggestionEntry() {
        GameObject newEntry = ResourceLoader.InstantiateObject("FoodSuggestionEntry", Vector2.zero, Quaternion.identity);
        newEntry.GetComponent<RectTransform>().SetParent(parentSuggestionsTo);

        FoodSuggestionEntry result = newEntry.GetComponent<FoodSuggestionEntry>();
        
        return result;
    }
    
    void PopulateSuggestions(UserData userData) {
        ClearEntries();
        
        FoodSuggestionEntry personalFavorite = InstantiateFoodSuggestionEntry();
        personalFavorite.PopulatePersonalFavorite(userData);
        
        InstantiateFoodSuggestionEntry().PopulateFamiliarSuggestion(userData);
        InstantiateFoodSuggestionEntry().PopulateUnfamiliarSuggestion(userData);

        List<UserData> friends = userData.GetFriends();
        
        foreach (var friend in friends) {
            InstantiateFoodSuggestionEntry().PopulateFriendSuggestion(friend);
        }

        int entryCount = friends.Count + 3;
        
        Vector2 newSize = new Vector2(
            parentSuggestionsTo.sizeDelta.x,
            personalFavorite.GetComponent<RectTransform>().sizeDelta.y * 
            entryCount + verticalLayoutGroup.spacing * entryCount);
            
        parentSuggestionsTo.sizeDelta = newSize;
    }
}