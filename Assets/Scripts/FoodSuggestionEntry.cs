using TMPro;
using UnityEngine;

public class FoodSuggestionEntry : MonoBehaviour {
    public TextMeshProUGUI dishNameText;
    public TextMeshProUGUI countryText;
    public TextMeshProUGUI noteText;
    
    public void PopulateFriendSuggestion(UserData friend) {
        PopulateGeneralInfo(friend.FavoriteDish());
        noteText.text = $"Recommended by {friend.GetFirstName()}";
    }

    public void PopulatePersonalFavorite(UserData user) {
        PopulateGeneralInfo(user.FavoriteDish());
        noteText.text = "Your personal favorite!";
    }

    public void PopulateFamiliarSuggestion(UserData user) {
        PopulateGeneralInfo(user.FamiliarSuggestion());
        noteText.text = "Something familiar";
    }

    public void PopulateUnfamiliarSuggestion(UserData user) {
        PopulateGeneralInfo(user.UnfamiliarSuggestion());
        noteText.text = "Something new";
    }
    
    public void PopulateGeneralInfo(Dish dish) {
        dishNameText.text = dish.GetName();
        countryText.text = dish.GetCountryNameFormatted();
    }
}