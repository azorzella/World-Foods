using TMPro;
using UnityEngine;

public class FoodSuggestionEntry : MonoBehaviour {
    public TextMeshProUGUI dishNameText;
    public TextMeshProUGUI countryText;
    public TextMeshProUGUI noteText;

    public void Initialize(UserData user) {
        Dish dish = user.FavoriteDish();

        dishNameText.text = dish.GetName();
        countryText.text = dish.GetCountryNameFormatted();
    }
}