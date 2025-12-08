using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummaryAndSuggestions : MonoBehaviour, MenuListener {
    public TextMeshProUGUI countryCountText;
    public TextMeshProUGUI dishCountText;
    public RectTransform parentSuggestionsTo;
    
    CanvasGroup canvasGroup;

    readonly List<GameObject> suggestionEntries = new();
    
    VerticalLayoutGroup verticalLayoutGroup;

    UserData currentUser;
    
    void Start() {
        CacheComponents();
    }

    /// <summary>
    /// Caches its canvas group, the vertical layout group on the suggestion parent,
    /// and the current user from the first instance of WorldMapVisualization it
    /// can find. It then registers itself to listen to the first instance of Menu
    /// it can find
    /// </summary>
    void CacheComponents() {
        canvasGroup = GetComponent<CanvasGroup>();
        verticalLayoutGroup = parentSuggestionsTo.GetComponent<VerticalLayoutGroup>();
        currentUser = FindFirstObjectByType<WorldMapVisualization>().GetCurrentUser();
        FindFirstObjectByType<Menu>().RegisterListener(this);
    }

    /// <summary>
    /// Updates the summary and populates its dish suggestions according to the
    /// passed user's data, then makes itself visible
    /// </summary>
    /// <param name="user"></param>
    void Show(UserData user) {
        ClearEntries();
        UpdateSummary(user);
        PopulateSuggestions(user);
        
        SetVisible(true);
    }

    /// <summary>
    /// Makes itself invisible
    /// </summary>
    void Hide() {
        SetVisible(false);
    }
    
    /// <summary>
    /// Sets the alpha, interactability, and raycast blocking flag of
    /// the cached canvas group according to whether it's visible or
    /// not
    /// </summary>
    /// <param name="visible"></param>
    void SetVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    /// <summary>
    /// Updates the country and dish count texts to reflect the passed user's data
    /// </summary>
    /// <param name="userData"></param>
    void UpdateSummary(UserData userData) {
        countryCountText.text = $"{userData.NumUniqueCountriesEatenFrom()} Countries";
        dishCountText.text = $"{userData.NumUniqueDishesEaten()} Dishes";
    }

    /// <summary>
    /// Clears all the entries in the list of cached suggestion entry objects
    /// </summary>
    void ClearEntries() {
        while (suggestionEntries.Count > 0) {
            Destroy(suggestionEntries[0]);
            suggestionEntries.RemoveAt(0);
        }
    }
    
    /// <summary>
    /// Instantiates a new FoodSuggestionEntry object, sets its parent to be the suggestion parent, and then returns
    /// its FoodSuggestionEntry component
    /// </summary>
    /// <returns></returns>
    FoodSuggestionEntry InstantiateFoodSuggestionEntry() {
        GameObject newEntry = ResourceLoader.InstantiateObject("FoodSuggestionEntry", Vector2.zero, Quaternion.identity);
        newEntry.GetComponent<RectTransform>().SetParent(parentSuggestionsTo);
        suggestionEntries.Add(newEntry);

        FoodSuggestionEntry result = newEntry.GetComponent<FoodSuggestionEntry>();
        
        return result;
    }
    
    /// <summary>
    /// Populates the user's food suggestions with their favorite food, a familiar suggestion, an unfamiliar
    /// suggestion, and all of their friends' favorite dishes. It then resizes the suggestion parent to fit
    /// the suggestion objects
    /// </summary>
    /// <param name="userData"></param>
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

    /// <summary>
    /// Shows the menu when the current menu tab selected is equal to one
    /// and hides it otherwise
    /// </summary>
    /// <param name="currentIndex"></param>
    public void NotifyMenuStateChanged(int currentIndex) {
        if (currentIndex == 1) {
            Show(currentUser);
        } else {
            Hide();
        }
    }
}