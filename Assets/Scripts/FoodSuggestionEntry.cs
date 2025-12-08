using TMPro;
using UnityEngine;

public class FoodSuggestionEntry : MonoBehaviour {
    public TextMeshProUGUI dishNameText;
    public TextMeshProUGUI countryText;
    public TextMeshProUGUI noteText;
    
    /// <summary>
    /// Populates itself with the passed user's favorite dish and sets
    /// its note to display who that dish was recommended by
    /// </summary>
    /// <param name="friend"></param>
    public void PopulateFriendSuggestion(UserData friend) {
        PopulateGeneralInfo(friend.GetFavoriteDish());
        noteText.text = $"Recommended by {friend.GetFirstName()}";
    }

    /// <summary>
    /// Populates itself with the passed user's favorite dish and sets
    /// its note to display that it's their favorite dish
    /// </summary>
    /// <param name="user"></param>
    public void PopulatePersonalFavorite(UserData user) {
        PopulateGeneralInfo(user.GetFavoriteDish());
        noteText.text = "Your personal favorite!";
    }

    /// <summary>
    /// Populates itself with a suggestion from a country the user
    /// has already eaten from and then updates the note to display
    /// that it's a suggestion from a familiar country
    /// </summary>
    /// <param name="user"></param>
    public void PopulateFamiliarSuggestion(UserData user) {
        PopulateGeneralInfo(user.FamiliarSuggestion());
        noteText.text = "Something familiar";
    }

    /// <summary>
    /// Populates itself with a suggestion from a country the user
    /// hasn't already eaten from and then updates the note to display
    /// that it's a suggestion from an unfamiliar country
    /// </summary>
    /// <param name="user"></param>
    public void PopulateUnfamiliarSuggestion(UserData user) {
        PopulateGeneralInfo(user.UnfamiliarSuggestion());
        noteText.text = "Something new";
    }
    
    /// <summary>
    /// Sets the dish and country name text to the passed dish's
    /// name and country of origin
    /// </summary>
    /// <param name="dish"></param>
    void PopulateGeneralInfo(Dish dish) {
        if (dish == null) {
            Debug.Log($"Dish was null.");
            return;
        }
        
        dishNameText.text = dish.GetName();
        countryText.text = dish.GetCountryNameFormatted();
    }
}